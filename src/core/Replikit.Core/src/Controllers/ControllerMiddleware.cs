using System.Reflection;
using System.Runtime.ExceptionServices;
using Kantaiko.Controllers;
using Kantaiko.Controllers.Execution;
using Kantaiko.Controllers.Introspection.Factory;
using Kantaiko.Controllers.ParameterConversion;
using Kantaiko.Controllers.ParameterConversion.Text;
using Kantaiko.Controllers.Result;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Replikit.Abstractions.Messages.Events;
using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Messages.Models.TextTokens;
using Replikit.Core.Controllers.Configuration;
using Replikit.Core.Controllers.Context;
using Replikit.Core.Controllers.ExecutionHandlers;
using Replikit.Core.Controllers.Options;
using Replikit.Core.Resources;
using Replikit.Core.Routing;
using Replikit.Core.Routing.Context;
using Replikit.Core.Routing.Middleware;

namespace Replikit.Core.Controllers;

internal class ControllerMiddleware : IBotEventMiddleware
{
    private readonly IControllerHandler<IMessageControllerContext> _controllerHandler;
    private readonly ILogger<ControllerMiddleware> _logger;

    public ControllerMiddleware(Assembly assembly, IServiceProvider serviceProvider,
        Action<ControllerConfigurationBuilder>? localConfigureDelegate)
    {
        var introspectionBuilder = new IntrospectionBuilder<IMessageControllerContext>();

        introspectionBuilder.SetServiceProvider(serviceProvider);
        introspectionBuilder.AddDefaultTransformation();
        introspectionBuilder.AddEndpointMatching();

        var handlers = new HandlerCollection<IMessageControllerContext>();

        handlers.AddEndpointMatching();
        handlers.AddParameterConversion(h => h.AddTextParameterConversion(ServiceHandlerFactory.Instance));
        handlers.AddControllerInstantiation(ServiceHandlerFactory.Instance);
        handlers.Add(new InvokeHandlerInstanceInterceptorsHandler<IMessageControllerContext>());
        handlers.AddEndpointInvocation();
        handlers.AddRequestCompletion();

        var configurationBuilder = new ControllerConfigurationBuilder(introspectionBuilder, handlers);

        var controllerOptions = serviceProvider.GetRequiredService<IOptions<GlobalControllerOptions>>();

        foreach (var configureDelegate in controllerOptions.Value.ConfigureDelegates)
        {
            configureDelegate(configurationBuilder);
        }

        localConfigureDelegate?.Invoke(configurationBuilder);

        var converterCollection = new TextParameterConverterCollection(configurationBuilder.ConverterLookupTypes);
        introspectionBuilder.AddTextParameterConversion(converterCollection);

        var lookupTypes = assembly.GetTypes();

        var introspectionInfo = introspectionBuilder.CreateIntrospectionInfo(lookupTypes);

        _controllerHandler = ControllerHandlerFactory.CreateControllerHandler(introspectionInfo, handlers);
        _logger = serviceProvider.GetRequiredService<ILogger<ControllerMiddleware>>();
    }

    public async Task HandleAsync(IBotEventContext context, BotEventDelegate next)
    {
        if (context is not IBotEventContext<MessageReceivedEvent> eventContext)
        {
            await next(context);
            return;
        }

        var controllerContext = new MessageControllerContext(eventContext);

        var result = await _controllerHandler.HandleAsync(
            controllerContext,
            context.ServiceProvider,
            context.CancellationToken
        );

        if (!result.IsMatched)
        {
            await next(context);
            return;
        }

        var response = CreateResponse(result);

        if (response is not null)
        {
            try
            {
                await context.MessageCollection.SendAsync(response, cancellationToken: context.CancellationToken);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "An error occurred while sending controller response");
            }
        }
    }

    private static OutMessage? CreateResponse(ControllerExecutionResult result)
    {
        switch (result)
        {
            case { ReturnValue: OutMessage outMessage }:
                return outMessage;
            case { ExitReason: ErrorExitReason errorExitReason }:
                return CreateErrorResponse(errorExitReason);
            case { ExitReason: ExceptionExitReason exceptionExitReason }:
                ExceptionDispatchInfo.Capture(exceptionExitReason.Exception).Throw();
                return null;
            default:
                return null;
        }
    }

    private static OutMessage CreateErrorResponse(ErrorExitReason errorExitReason)
    {
        var message = errorExitReason switch
        {
            { Parameter: not null } =>
                $"{string.Format(Locale.InvalidParameter, errorExitReason.Parameter.Name)}\n" +
                errorExitReason.ErrorMessage,
            _ => errorExitReason.ErrorMessage
        };

        return TextToken.Code(message ?? "Unknown error");
    }
}
