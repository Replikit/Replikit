using Kantaiko.Controllers.Execution;
using Kantaiko.Controllers.Execution.Handlers;
using Kantaiko.Controllers.Result;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Controllers.Context;
using Replikit.Extensions.State.Implementation;

namespace Replikit.Extensions.State.ExecutionHandlers;

internal class LoadStateHandler : ControllerExecutionHandler<IMessageControllerContext>
{
    protected override async Task<ControllerResult> HandleAsync(
        ControllerContext<IMessageControllerContext> context,
        NextAction next)
    {
        var stateLoader = context.ServiceProvider.GetRequiredService<IStateLoader>();

        await stateLoader.LoadAsync(context.CancellationToken);

        return await next();
    }
}
