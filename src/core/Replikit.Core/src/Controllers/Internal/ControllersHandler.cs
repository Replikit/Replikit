using Kantaiko.Controllers.Result;
using Kantaiko.Routing;
using Kantaiko.Routing.Events;
using Microsoft.Extensions.Logging;
using Replikit.Abstractions.Messages.Events;
using Replikit.Abstractions.Messages.Models;
using Replikit.Core.Handlers;
using Replikit.Core.Resources;

namespace Replikit.Core.Controllers.Internal;

internal class ControllersHandler : MessageEventHandler<MessageReceivedEvent>
{
    private readonly ControllerHandlerAccessor _controllerHandlerAccessor;
    private readonly ILogger<ControllersHandler> _logger;

    public ControllersHandler(ControllerHandlerAccessor controllerHandlerAccessor, ILogger<ControllersHandler> logger)
    {
        _controllerHandlerAccessor = controllerHandlerAccessor;
        _logger = logger;
    }

    protected override async Task<Unit> HandleAsync(IEventContext<MessageReceivedEvent> context, NextAction next)
    {
        var result = await _controllerHandlerAccessor.Handler.Handle(context);

        if (!result.IsMatched)
        {
            return await next();
        }

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

    private static OutMessage? CreateResponse(ControllerExecutionResult result) => result switch
    {
        { ReturnValue: OutMessage outMessage } => outMessage,
        { ExitReason: ErrorExitReason errorExitReason } => CreateErrorResponse(errorExitReason),
        { ExitReason: ExceptionExitReason exceptionExitReason } => throw exceptionExitReason.Exception,
        _ => null
    };

    private static OutMessage CreateErrorResponse(ErrorExitReason errorExitReason)
    {
        var message = errorExitReason switch
        {
            { Parameter: not null } =>
                $"{string.Format(Locale.InvalidParameter, errorExitReason.Parameter.Name)}\n" +
                errorExitReason.ErrorMessage,
            _ => errorExitReason.ErrorMessage
        };

        return OutMessage.FromCode(message ?? "Unexpected error");
    }
}
