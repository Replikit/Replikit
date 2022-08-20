using Replikit.Abstractions.Channels.Models;
using Replikit.Abstractions.Events;

namespace Replikit.Abstractions.Channels.Events;

/// <summary>
/// Represents an event that occurred in a channel.
/// </summary>
public interface IChannelEvent : IBotEvent
{
    /// <summary>
    /// The channel that the event occurred in.
    /// </summary>
    ChannelInfo Channel { get; }
}
