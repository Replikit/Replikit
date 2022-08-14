using Replikit.Abstractions.Events;

namespace Replikit.Core.Routing.Context;

internal class AdapterEventContextAccessor : IAdapterEventContextAccessor
{
    public IAdapterEventContext<IAdapterEvent>? CurrentContext { get; set; }
}
