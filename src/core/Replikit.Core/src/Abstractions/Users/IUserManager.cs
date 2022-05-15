namespace Replikit.Core.Abstractions.Users;

public interface IUserManager<TUser>
{
    Task<TUser?> GetCurrentUserOrDefaultAsync(CancellationToken cancellationToken = default);
}
