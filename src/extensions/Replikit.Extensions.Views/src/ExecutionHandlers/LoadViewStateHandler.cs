using Kantaiko.Controllers.Execution;
using Kantaiko.Controllers.Execution.Handlers;
using Kantaiko.Controllers.Result;
using Replikit.Extensions.Views.Internal;

namespace Replikit.Extensions.Views.ExecutionHandlers;

internal class LoadViewStateHandler : ControllerExecutionHandler<ViewContext>
{
    protected override Task<ControllerExecutionResult> HandleAsync(ControllerExecutionContext<ViewContext> context,
        NextAction next)
    {
        var controller = context.ControllerInstance!;

        if (controller is not IStatefulView statefulView)
        {
            return next();
        }

        var state = context.RequestContext.Request.ViewInstance?.State.GetValue(statefulView.StateType);
        state ??= Activator.CreateInstance(statefulView.StateType);

        if (state is null)
        {
            throw new InvalidOperationException("Failed to retrieve or create state for the stateful view");
        }

        statefulView.SetState(state);

        return next();
    }
}
