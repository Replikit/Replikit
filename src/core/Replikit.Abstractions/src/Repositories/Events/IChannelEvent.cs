using Replikit.Abstractions.Events;
using Replikit.Abstractions.Repositories.Models;

namespace Replikit.Abstractions.Repositories.Events;

public interface IChannelEvent : IEvent
{
    ChannelInfo Channel { get; }
}
