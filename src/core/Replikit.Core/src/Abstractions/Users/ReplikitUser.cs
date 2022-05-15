using System.Collections.Immutable;
using Replikit.Abstractions.Common.Models;

namespace Replikit.Core.Abstractions.Users;

public class ReplikitUser : ReplikitUser<Guid>
{
    public ReplikitUser()
    {
        // ReSharper disable once VirtualMemberCallInConstructor
        Id = Guid.NewGuid();
    }

    public ReplikitUser(string username) : this()
    {
        // ReSharper disable once VirtualMemberCallInConstructor
        Username = username;
    }
}

public class ReplikitUser<TId>
{
    public ReplikitUser() { }

    public ReplikitUser(string username) : this()
    {
        // ReSharper disable once VirtualMemberCallInConstructor
        Username = username;
    }

    public virtual TId Id { get; protected init; } = default!;
    public virtual string Username { get; set; } = default!;

    public virtual IReadOnlyList<GlobalIdentifier> AccountIds { get; protected set; } = null!;

    public virtual void AddAccount(GlobalIdentifier accountId)
    {
        if (!AccountIds.Contains(accountId))
        {
            AccountIds = AccountIds.ToImmutableArray().Add(accountId);
        }
    }

    public virtual void RemoveAccount(GlobalIdentifier accountId)
    {
        AccountIds = AccountIds.ToImmutableArray().Remove(accountId);
    }
}
