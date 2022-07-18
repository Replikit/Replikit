using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Events;

public interface IAdapterEvent
{
    public AdapterIdentifier AdapterId { get; }
}
