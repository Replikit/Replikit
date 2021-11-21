using Replikit.Abstractions.Repositories.Events;
using Replikit.Abstractions.Repositories.Models;

namespace Replikit.Core.Handlers;

public abstract class AccountEventHandler<TEvent> : AdapterEventHandler<TEvent> where TEvent : IAccountEvent
{
    protected AccountInfo Account => Event.Account;
}
