using Kantaiko.Controllers.Execution;
using Kantaiko.Controllers.Execution.Handlers;
using Kantaiko.Controllers.Result;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Extensions.State.Implementation;

namespace Replikit.Extensions.Scenes.ExecutionHandlers;

internal class LoadStateHandler : ControllerExecutionHandler<SceneContext>
{
    protected override async Task<ControllerExecutionResult> HandleAsync(
        ControllerExecutionContext<SceneContext> context, NextAction next)
    {
        var stateLoader = context.ServiceProvider.GetRequiredService<IStateLoader>();

        await stateLoader.LoadAsync(context.CancellationToken);

        return await next();
    }
}
