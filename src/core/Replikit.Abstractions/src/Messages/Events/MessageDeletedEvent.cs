using Replikit.Abstractions.Accounts.Models;
using Replikit.Abstractions.Channels.Events;
using Replikit.Abstractions.Channels.Models;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;

namespace Replikit.Abstractions.Messages.Events;

/// <summary>
/// Represents an event occurred when a message is deleted.
/// </summary>
public class MessageDeletedEvent : ChannelAndAccountEvent
{
    /// <summary>
    /// Creates a new instance of <see cref="MessageDeletedEvent"/>.
    /// </summary>
    /// <param name="botId">A bot identifier.</param>
    /// <param name="channel">A channel.</param>
    /// <param name="account">An account.</param>
    public MessageDeletedEvent(BotIdentifier botId, ChannelInfo channel, AccountInfo account) :
        base(botId, channel, account) { }

    /// <summary>
    /// The message that was deleted.
    /// <br/>
    /// Most likely this will be null, but in some scenarios platform might give access to the deleted message.
    /// </summary>
    public Message? Message { get; init; }
}
