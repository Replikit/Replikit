using Kantaiko.Controllers.Middleware;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Abstractions.Messages.Events;
using Replikit.Core.Handlers;
using Replikit.Extensions.Sessions.Internal;

namespace Replikit.Extensions.Sessions.ControllerMiddleware.BeforeExecution;

internal class LoadSessionsHandler : EndpointMiddleware<IEventContext<MessageReceivedEvent>>
{
    public override EndpointMiddlewareStage Stage => EndpointMiddlewareStage.BeforeExecution;

    public override async Task HandleAsync(EndpointMiddlewareContext<IEventContext<MessageReceivedEvent>> context,
        CancellationToken cancellationToken)
    {
        var sessionManager = context.ServiceProvider.GetRequiredService<SessionManager>();
        await sessionManager.LoadAsync(cancellationToken);
    }
}
