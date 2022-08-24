using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Routing;
using Replikit.Core.Routing.Context;
using Replikit.Core.Routing.Middleware;

namespace Replikit.Extensions.State.Implementation;

internal class SaveStateMiddleware : IBotEventMiddleware
{
    public async Task HandleAsync(IBotEventContext context, BotEventDelegate next)
    {
        await next(context);

        var loader = context.ServiceProvider.GetRequiredService<IStateLoader>();

        await loader.SaveAsync(context.CancellationToken);
    }
}
