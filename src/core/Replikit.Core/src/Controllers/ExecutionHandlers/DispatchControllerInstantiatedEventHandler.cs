using Kantaiko.Controllers.Execution;
using Kantaiko.Controllers.Execution.Handlers;
using Kantaiko.Controllers.Result;
using Kantaiko.Routing.Events;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Abstractions.Messages.Events;
using Replikit.Core.Controllers.Lifecycle;

namespace Replikit.Core.Controllers.ExecutionHandlers;

public class DispatchControllerInstantiatedEventHandler :
    ControllerExecutionHandler<IEventContext<MessageReceivedEvent>>
{
    protected override async Task<ControllerExecutionResult> HandleAsync(
        ControllerExecutionContext<IEventContext<MessageReceivedEvent>> context, NextAction next)
    {
        var lifecycle = context.ServiceProvider.GetRequiredService<IControllerLifecycle>();

        await using var scope = context.ServiceProvider.CreateAsyncScope();

        var eventContext = new EventContext<ControllerInstantiatedEvent>(
            new ControllerInstantiatedEvent(context.RequestContext),
            scope.ServiceProvider, cancellationToken: context.CancellationToken);

        await lifecycle.ControllerInstantiated.Handle(eventContext);

        return await next();
    }
}
