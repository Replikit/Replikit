using Kantaiko.Controllers.Execution;
using Kantaiko.Controllers.Execution.Handlers;
using Kantaiko.Controllers.Result;

namespace Replikit.Extensions.Views.ExecutionHandlers;

internal class LoadViewEndpointParametersHandler : ControllerExecutionHandler<ViewContext>
{
    protected override Task<ControllerExecutionResult> HandleAsync(ControllerExecutionContext<ViewContext> context,
        NextAction next)
    {
        var parameters = context.Endpoint!.Parameters;

        context.ConstructedParameters = context.RequestContext.Request.Parameters
            .Select((value, index) => value.GetValue(parameters[index].ParameterType))
            .ToArray();

        return next();
    }
}
