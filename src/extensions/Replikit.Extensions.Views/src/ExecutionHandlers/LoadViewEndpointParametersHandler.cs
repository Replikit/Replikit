using Kantaiko.Controllers.Execution;
using Kantaiko.Controllers.Execution.Handlers;
using Kantaiko.Controllers.Result;

namespace Replikit.Extensions.Views.ExecutionHandlers;

internal class LoadViewEndpointParametersHandler : ControllerExecutionHandler<ViewContext>
{
    protected override Task<ControllerExecutionResult> HandleAsync(ControllerExecutionContext<ViewContext> context,
        NextAction next)
    {
        context.ConstructedParameters = context.RequestContext.Request.Parameters;

        return next();
    }
}
