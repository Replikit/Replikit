using Kantaiko.Controllers.Result;
using Kantaiko.Hosting.Hooks;
using Kantaiko.Routing;
using Microsoft.Extensions.Logging;
using Replikit.Abstractions.Messages.Events;
using Replikit.Abstractions.Messages.Models;
using Replikit.Core.Controllers.Hooks;
using Replikit.Core.Handlers;
using Replikit.Core.Resources;

namespace Replikit.Core.Controllers.Internal;

internal class ControllersHandler : MessageEventHandler<MessageReceivedEvent>
{
    private readonly RequestHandlerAccessor _requestHandlerAccessor;
    private readonly IHookDispatcher _hookDispatcher;
    private readonly ILogger<ControllersHandler> _logger;

    public ControllersHandler(RequestHandlerAccessor requestHandlerAccessor, IHookDispatcher hookDispatcher,
        ILogger<ControllersHandler> logger)
    {
        _requestHandlerAccessor = requestHandlerAccessor;
        _hookDispatcher = hookDispatcher;
        _logger = logger;
    }

    public override async Task<Unit> HandleAsync(IEventContext<MessageReceivedEvent> context, NextAction next)
    {
        await _hookDispatcher.DispatchAsync(new RequestHandlingHook(context), CancellationToken);

        var result = await _requestHandlerAccessor.RequestHandler.HandleAsync(
            context, ServiceProvider, CancellationToken);

        if (!result.IsMatched)
        {
            return await next();
        }

        var requestHandledHook = new RequestHandledHook(context, result);
        await _hookDispatcher.DispatchAsync(requestHandledHook, CancellationToken);

        if (!requestHandledHook.ShouldRespond) return default;

        var response = CreateResponse(result);

        if (response is not null)
        {
            try
            {
                await MessageCollection.SendAsync(response, cancellationToken: CancellationToken);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "An error occurred while sending controller response");
            }
        }

        return default;
    }

    private static OutMessage? CreateResponse(RequestProcessingResult result) => result switch
    {
        { ReturnValue: OutMessage outMessage } => outMessage,
        { ExitReason: ErrorExitReason errorExitReason } => CreateErrorResponse(errorExitReason),
        _ => null
    };

    private static OutMessage CreateErrorResponse(ErrorExitReason errorExitReason)
    {
        var message = errorExitReason switch
        {
            { Parameter: not null } and { Stage: not RequestErrorStage.ParameterExistenceCheck } =>
                $"{string.Format(Locale.InvalidParameter, errorExitReason.Parameter.Name)}\n" +
                errorExitReason.ErrorMessage,
            _ => errorExitReason.ErrorMessage
        };

        return OutMessage.FromCode(message);
    }
}
