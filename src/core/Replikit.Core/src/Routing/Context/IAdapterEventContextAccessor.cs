using Replikit.Abstractions.Events;

namespace Replikit.Core.Routing.Context;

public interface IAdapterEventContextAccessor
{
    IAdapterEventContext<IBotEvent>? CurrentContext { get; }
}
