using Replikit.Abstractions.Accounts.Models;
using Replikit.Abstractions.Channels.Models;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;

namespace Replikit.Abstractions.Messages.Events;

/// <summary>
/// Represents an event occurred when a message is received.
/// </summary>
public class MessageReceivedEvent : MessageEvent
{
    /// <summary>
    /// Creates a new instance of <see cref="MessageReceivedEvent"/>.
    /// </summary>
    /// <param name="botId">A bot identifier.</param>
    /// <param name="message">A message.</param>
    /// <param name="channel">A channel.</param>
    /// <param name="account">An account.</param>
    public MessageReceivedEvent(BotIdentifier botId, Message message, ChannelInfo channel, AccountInfo account) :
        base(botId, message, channel, account) { }
}
