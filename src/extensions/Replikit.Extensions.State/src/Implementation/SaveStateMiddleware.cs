using Microsoft.Extensions.DependencyInjection;
using Replikit.Abstractions.Events;
using Replikit.Core.Routing;
using Replikit.Core.Routing.Context;
using Replikit.Core.Routing.Middleware;

namespace Replikit.Extensions.State.Implementation;

internal class SaveStateMiddleware : IAdapterEventMiddleware
{
    public async Task HandleAsync(IAdapterEventContext<IBotEvent> context, AdapterEventDelegate next)
    {
        await next(context);

        var loader = context.ServiceProvider.GetRequiredService<IStateLoader>();

        await loader.SaveAsync(context.CancellationToken);
    }
}
