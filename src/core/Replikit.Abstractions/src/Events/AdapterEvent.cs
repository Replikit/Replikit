using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Events;

public abstract class AdapterEvent : IAdapterEvent
{
    protected AdapterEvent(AdapterIdentifier adapterId)
    {
        AdapterId = adapterId;
    }

    public AdapterIdentifier AdapterId { get; }
}
