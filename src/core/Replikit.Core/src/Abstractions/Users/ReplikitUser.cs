using System.Collections.Immutable;
using Replikit.Abstractions.Common.Models;

// ReSharper disable VirtualMemberCallInConstructor

namespace Replikit.Core.Abstractions.Users;

public class ReplikitUser : ReplikitUser<Guid>
{
    public ReplikitUser()
    {
        Id = Guid.NewGuid();
    }

    public ReplikitUser(string username) : this()
    {
        Username = username;
    }
}

public class ReplikitUser<TId>
{
    public ReplikitUser()
    {
        AccountIds = ImmutableList<GlobalIdentifier>.Empty;
    }

    public ReplikitUser(string username) : this()
    {
        Username = username;
    }

    public virtual TId Id { get; protected init; } = default!;
    public virtual string? Username { get; set; }

    public virtual IReadOnlyList<GlobalIdentifier> AccountIds { get; protected set; } = null!;

    public virtual void AddAccount(GlobalIdentifier accountId)
    {
        if (!AccountIds.Contains(accountId))
        {
            AccountIds = AccountIds.ToImmutableList().Add(accountId);
        }
    }

    public virtual void RemoveAccount(GlobalIdentifier accountId)
    {
        AccountIds = AccountIds.ToImmutableList().Remove(accountId);
    }
}
