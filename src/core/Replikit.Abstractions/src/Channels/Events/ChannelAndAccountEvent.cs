using Replikit.Abstractions.Accounts.Events;
using Replikit.Abstractions.Accounts.Models;
using Replikit.Abstractions.Channels.Models;
using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Channels.Events;

/// <summary>
/// Represents an event occurred in a channel with an account.
/// </summary>
public abstract class ChannelAndAccountEvent : ChannelEvent, IAccountEvent
{
    /// <summary>
    /// Creates a new instance of <see cref="ChannelAndAccountEvent"/>.
    /// </summary>
    /// <param name="botId">A bot identifier.</param>
    /// <param name="channel">A channel.</param>
    /// <param name="account">An account.</param>
    protected ChannelAndAccountEvent(BotIdentifier botId, ChannelInfo channel, AccountInfo account) :
        base(botId, channel)
    {
        Account = account;
    }

    public AccountInfo Account { get; }
}
