using Kantaiko.Routing;
using Kantaiko.Routing.Handlers;
using Replikit.Abstractions.Events;
using Replikit.Core.Handlers.Context;

namespace Replikit.Core.Handlers;

internal class AdapterEventRouter
{
    public IChainedHandler<IAdapterEventContext<AdapterEvent>, Task<Unit>> Handler { get; set; } = default!;
}
