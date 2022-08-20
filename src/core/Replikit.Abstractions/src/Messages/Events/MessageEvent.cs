using Replikit.Abstractions.Accounts.Models;
using Replikit.Abstractions.Channels.Events;
using Replikit.Abstractions.Channels.Models;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;

namespace Replikit.Abstractions.Messages.Events;

/// <summary>
/// Represents an event that is raised when something occurred with a message and this message is always accessible.
/// </summary>
public abstract class MessageEvent : ChannelAndAccountEvent
{
    /// <summary>
    /// Creates a new instance of <see cref="MessageEvent"/>.
    /// </summary>
    /// <param name="botId">A bot identifier.</param>
    /// <param name="message">A message.</param>
    /// <param name="channel">A channel.</param>
    /// <param name="account">An account.</param>
    protected MessageEvent(BotIdentifier botId, Message message, ChannelInfo channel, AccountInfo account) :
        base(botId, channel, account)
    {
        Message = message;
    }

    /// <summary>
    /// The message associated with the event.
    /// </summary>
    public Message Message { get; }
}
