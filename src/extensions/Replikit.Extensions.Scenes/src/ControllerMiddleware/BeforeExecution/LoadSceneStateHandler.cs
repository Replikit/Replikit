using System.Diagnostics;
using Kantaiko.Controllers.Middleware;
using Replikit.Extensions.Scenes.Internal;

namespace Replikit.Extensions.Scenes.ControllerMiddleware.BeforeExecution;

internal class LoadSceneStateHandler : EndpointMiddleware<SceneContext>
{
    public override EndpointMiddlewareStage Stage => EndpointMiddlewareStage.BeforeExecution;

    public override Task HandleAsync(EndpointMiddlewareContext<SceneContext> context,
        CancellationToken cancellationToken)
    {
        var controller = context.ExecutionContext.ControllerInstance;
        Debug.Assert(controller is not null);

        if (controller is not IStatefulScene statefulScene) return Task.CompletedTask;

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

        return Task.CompletedTask;
    }
}
