using Replikit.Abstractions.Events;

namespace Replikit.Core.Routing.Context;

internal class AdapterEventContextAccessor : IAdapterEventContextAccessor
{
    public IAdapterEventContext<IBotEvent>? CurrentContext { get; set; }
}
