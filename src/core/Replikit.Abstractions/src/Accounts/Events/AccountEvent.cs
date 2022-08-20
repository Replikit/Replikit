using Replikit.Abstractions.Accounts.Models;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Events;

namespace Replikit.Abstractions.Accounts.Events;

/// <summary>
/// <inheritdoc cref="IAccountEvent"/>
/// </summary>
public abstract class AccountEvent : BotEvent, IAccountEvent
{
    /// <summary>
    /// Creates a new instance of <see cref="AccountEvent"/>.
    /// </summary>
    /// <param name="botId">A bot identifier.</param>
    /// <param name="account">An account.</param>
    protected AccountEvent(BotIdentifier botId, AccountInfo account) : base(botId)
    {
        Account = account;
    }

    public AccountInfo Account { get; }
}
