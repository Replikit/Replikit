using Kantaiko.Controllers.Execution;
using Kantaiko.Controllers.Execution.Handlers;
using Kantaiko.Controllers.Result;

namespace Replikit.Extensions.Scenes.ExecutionHandlers;

internal class LoadSceneEndpointParametersHandler : ControllerExecutionHandler<SceneContext>
{
    protected override Task<ControllerExecutionResult> HandleAsync(ControllerExecutionContext<SceneContext> context,
        NextAction next)
    {
        var parameters = context.Endpoint!.Parameters;

        context.ConstructedParameters = context.RequestContext.Request.Stage.Parameters
            .Select((value, index) => value.GetValue(parameters[index].ParameterType))
            .ToArray();

        return next();
    }
}
