using Kantaiko.Controllers.Execution;
using Kantaiko.Controllers.Execution.Handlers;
using Kantaiko.Controllers.Result;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Extensions.State.Implementation;

namespace Replikit.Extensions.Views.ExecutionHandlers;

internal class LoadStateHandler : ControllerExecutionHandler<ViewContext>
{
    protected override async Task<ControllerResult> HandleAsync(ControllerContext<ViewContext> context, NextAction next)
    {
        var stateLoader = context.ServiceProvider.GetRequiredService<IStateLoader>();

        await stateLoader.LoadAsync(context.CancellationToken);

        return await next();
    }
}
