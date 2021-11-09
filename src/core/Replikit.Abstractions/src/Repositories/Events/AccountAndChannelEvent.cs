using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Repositories.Models;

namespace Replikit.Abstractions.Repositories.Events;

public abstract class AccountAndChannelEvent : ChannelEvent, IAccountEvent
{
    public AccountAndChannelEvent(AdapterIdentifier adapterId, ChannelInfo channel, AccountInfo account) : base(
        adapterId,
        channel)
    {
        Account = account;
    }

    public AccountInfo Account { get; }
}
