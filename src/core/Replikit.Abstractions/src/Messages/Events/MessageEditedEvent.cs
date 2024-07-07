using Replikit.Abstractions.Accounts.Models;
using Replikit.Abstractions.Channels.Models;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;

namespace Replikit.Abstractions.Messages.Events;

/// <summary>
/// Represents an event occurred when a message is edited.
/// </summary>
public class MessageEditedEvent : MessageEvent
{
    /// <summary>
    /// Creates a new instance of <see cref="MessageEditedEvent"/>.
    /// </summary>
    /// <param name="botId">A bot identifier.</param>
    /// <param name="message">A message.</param>
    /// <param name="channel">A channel.</param>
    /// <param name="account">An account.</param>
    public MessageEditedEvent(BotIdentifier botId, Message message, ChannelInfo channel, AccountInfo account) :
        base(botId, message, channel, account) { }

    /// <summary>
    /// The previous message.
    /// <br/>
    /// May be null if the platform is not tracking message history, or it is not accessible to the bot.
    /// </summary>
    public Message? OldMessage { get; init; }
}
