namespace Replikit.Core.Abstractions.Users;

public interface ICurrentUserManager<TUser, TUserId> where TUser : ReplikitUser<TUserId>
{
    Task<TUser?> GetCurrentUserOrDefaultAsync(CancellationToken cancellationToken = default);

    Task<TUser> EnsureCurrentUserCreatedAsync(CancellationToken cancellationToken = default);
}
