using Replikit.Abstractions.Channels.Models;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Events;

namespace Replikit.Abstractions.Channels.Events;

/// <summary>
/// <inheritdoc cref="IChannelEvent"/>
/// </summary>
public abstract class ChannelEvent : BotEvent, IChannelEvent
{
    /// <summary>
    /// Creates a new instance of <see cref="ChannelEvent"/>.
    /// </summary>
    /// <param name="botId">A bot identifier.</param>
    /// <param name="channel">A channel.</param>
    protected ChannelEvent(BotIdentifier botId, ChannelInfo channel) : base(botId)
    {
        Channel = channel;
    }

    /// <summary>
    /// <inheritdoc cref="IChannelEvent.Channel"/>
    /// </summary>
    public ChannelInfo Channel { get; }
}
