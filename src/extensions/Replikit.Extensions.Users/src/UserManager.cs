using Replikit.Abstractions.Common.Models;
using Replikit.Core.Abstractions.Users;

namespace Replikit.Extensions.Users;

public class UserManager<TUser, TUserId> where TUser : ReplikitUser<TUserId>, new()
{
    private readonly IUserStore<TUser, TUserId> _store;

    public UserManager(IUserStore<TUser, TUserId> store)
    {
        _store = store;
    }

    public virtual Task<TUser?> FindBydId(TUserId userId, CancellationToken cancellationToken = default)
    {
        return _store.FindByIdAsync(userId, cancellationToken);
    }

    public virtual Task<TUser?> FindByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return _store.FindByUsernameAsync(username, cancellationToken);
    }

    public virtual Task<TUser?> FindByAccountIdAsync(GlobalIdentifier accountId,
        CancellationToken cancellationToken = default)
    {
        return _store.FindByAccountIdAsync(accountId, cancellationToken);
    }

    public virtual async Task<TUser> EnsureUserCreatedAsync(GlobalIdentifier accountId,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(accountId);

        var user = await _store.FindByAccountIdAsync(accountId, cancellationToken);

        if (user is null)
        {
            user = new TUser();
            user.AddAccount(accountId);

            await _store.AddAsync(user, cancellationToken);
        }

        return user;
    }

    public virtual async Task<bool> ChangeUsernameAsync(TUser user, string newUsername,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(user);
        ArgumentNullException.ThrowIfNull(newUsername);

        var conflictingUser = await _store.FindByUsernameAsync(newUsername, cancellationToken);

        if (conflictingUser is not null)
        {
            return false;
        }

        user.Username = newUsername;
        await _store.UpdateAsync(user, cancellationToken);

        return true;
    }

    public virtual async Task<bool> AddAccountAsync(TUser user, GlobalIdentifier accountId,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(user);
        ArgumentNullException.ThrowIfNull(accountId);

        if (user.AccountIds.Contains(accountId))
        {
            return false;
        }

        user.AddAccount(accountId);
        await _store.UpdateAsync(user, cancellationToken);

        return true;
    }

    public virtual async Task<bool> RemoveAccountAsync(TUser user, GlobalIdentifier accountId,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(user);
        ArgumentNullException.ThrowIfNull(accountId);

        if (!user.AccountIds.Contains(accountId))
        {
            return false;
        }

        user.RemoveAccount(accountId);
        await _store.UpdateAsync(user, cancellationToken);

        return true;
    }

    public virtual async Task DeleteAsync(TUser user, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(user);

        await _store.DeleteAsync(user.Id, cancellationToken);
    }
}
