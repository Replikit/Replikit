using System.Diagnostics;
using Kantaiko.Controllers.Middleware;
using Replikit.Extensions.Views.Internal;

namespace Replikit.Extensions.Views.ControllerMiddleware.BeforeExecution;

internal class LoadViewStateHandler : EndpointMiddleware<ViewContext>
{
    public override EndpointMiddlewareStage Stage => EndpointMiddlewareStage.BeforeExecution;

    public override Task HandleAsync(EndpointMiddlewareContext<ViewContext> context,
        CancellationToken cancellationToken)
    {
        var controller = context.ExecutionContext.ControllerInstance;
        Debug.Assert(controller is not null);

        if (controller is not IStatefulView statefulView) return Task.CompletedTask;

        var state = context.RequestContext.Request.ViewInstance?.State.GetValue(statefulView.StateType);
        state ??= Activator.CreateInstance(statefulView.StateType);

        if (state is null)
        {
            throw new InvalidOperationException("Failed to retrieve or create state for the stateful view");
        }

        statefulView.SetState(state);

        return Task.CompletedTask;
    }
}
