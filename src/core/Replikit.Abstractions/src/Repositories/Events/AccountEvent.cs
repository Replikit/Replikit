using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Events;
using Replikit.Abstractions.Repositories.Models;

namespace Replikit.Abstractions.Repositories.Events;

public abstract class AccountEvent : Event, IAccountEvent
{
    public AccountEvent(AdapterIdentifier adapterId, AccountInfo account) : base(adapterId)
    {
        Account = account;
    }

    public AccountInfo Account { get; }
}
