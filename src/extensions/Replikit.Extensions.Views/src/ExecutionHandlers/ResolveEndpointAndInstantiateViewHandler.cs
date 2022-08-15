using Kantaiko.Controllers.Execution;
using Kantaiko.Controllers.Execution.Handlers;
using Replikit.Core.Routing;
using Replikit.Extensions.Views.Internal;

namespace Replikit.Extensions.Views.ExecutionHandlers;

internal class ResolveEndpointAndInstantiateViewHandler : IControllerExecutionHandler<InternalViewContext>
{
    public Task HandleAsync(ControllerExecutionContext<InternalViewContext> context)
    {
        context.Endpoint = context.RequestContext.ActionRequest?.Endpoint;

        context.ControllerInstance = ServiceHandlerFactory.Instance
            .CreateHandler(context.RequestContext.ViewController.Type, context.ServiceProvider);

        return Task.CompletedTask;
    }
}
