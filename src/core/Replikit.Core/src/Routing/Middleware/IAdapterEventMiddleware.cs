using Replikit.Abstractions.Events;
using Replikit.Core.Routing.Context;

namespace Replikit.Core.Routing.Middleware;

public interface IAdapterEventMiddleware
{
    Task HandleAsync(IAdapterEventContext<IBotEvent> context, AdapterEventDelegate next);
}
