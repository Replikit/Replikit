using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Events;

public interface IEvent
{
    public AdapterIdentifier AdapterId { get; }
}
