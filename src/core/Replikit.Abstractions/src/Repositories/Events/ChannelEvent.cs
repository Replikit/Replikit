using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Events;
using Replikit.Abstractions.Repositories.Models;

namespace Replikit.Abstractions.Repositories.Events;

public abstract class ChannelEvent : AdapterEvent, IChannelEvent
{
    public ChannelEvent(AdapterIdentifier adapterId, ChannelInfo channel) : base(adapterId)
    {
        Channel = channel;
    }

    public ChannelInfo Channel { get; }
}
