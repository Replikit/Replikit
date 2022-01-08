using Kantaiko.Controllers.Execution;
using Kantaiko.Controllers.Execution.Handlers;
using Kantaiko.Controllers.Result;
using Replikit.Extensions.Scenes.Internal;

namespace Replikit.Extensions.Scenes.ExecutionHandlers;

internal class LoadSceneStateHandler : ControllerExecutionHandler<SceneContext>
{
    protected override Task<ControllerExecutionResult> HandleAsync(ControllerExecutionContext<SceneContext> context,
        NextAction next)
    {
        var controller = context.ControllerInstance!;

        if (controller is not IStatefulScene statefulScene)
        {
            return next();
        }

        object? state = null;

        var request = context.RequestContext.Request;

        // Restore state only on same scenes
        if (request.Stage.SceneType == request.SceneInstance?.CurrentStage.SceneType)
        {
            state = context.RequestContext.Request.SceneInstance?.State.GetValue(statefulScene.StateType);
        }

        state ??= Activator.CreateInstance(statefulScene.StateType);

        if (state is null)
        {
            throw new InvalidOperationException("Failed to retrieve or create state for the stateful Scene");
        }

        statefulScene.SetState(state);

        return next();
    }
}
