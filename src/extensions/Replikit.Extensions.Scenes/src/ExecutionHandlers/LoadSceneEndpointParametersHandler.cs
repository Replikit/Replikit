using Kantaiko.Controllers.Execution;
using Kantaiko.Controllers.Execution.Handlers;
using Kantaiko.Controllers.Result;

namespace Replikit.Extensions.Scenes.ExecutionHandlers;

internal class LoadSceneEndpointParametersHandler : ControllerExecutionHandler<SceneContext>
{
    protected override Task<ControllerExecutionResult> HandleAsync(ControllerExecutionContext<SceneContext> context,
        NextAction next)
    {
        context.ConstructedParameters = context.RequestContext.Request.Stage.Parameters;

        return next();
    }
}
