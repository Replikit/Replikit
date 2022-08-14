using Replikit.Abstractions.Events;

namespace Replikit.Core.Routing.Context;

public interface IAdapterEventContextAccessor
{
    IAdapterEventContext<IAdapterEvent>? CurrentContext { get; }
}
