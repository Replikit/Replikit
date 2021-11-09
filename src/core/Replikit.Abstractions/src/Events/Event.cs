using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Events;

public abstract class Event : IEvent
{
    protected Event(AdapterIdentifier adapterId)
    {
        AdapterId = adapterId;
    }

    public AdapterIdentifier AdapterId { get; }
}
