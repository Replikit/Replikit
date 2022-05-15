using Replikit.Abstractions.Common.Models;

namespace Replikit.Core.Abstractions.Users;

public interface IUserStore : IUserStore<ReplikitUser, Guid> { }

public interface IUserStore<TUser, TUserId> where TUser : ReplikitUser<TUserId>
{
    Task<TUser?> FindByIdAsync(TUserId id, CancellationToken cancellationToken = default);

    Task<TUser?> FindByAccountIdAsync(GlobalIdentifier accountId, CancellationToken cancellationToken = default);

    Task<TUser> CreateAsync(TUser user, CancellationToken cancellationToken = default);
    Task<TUser> UpdateAsync(TUser user, CancellationToken cancellationToken = default);

    Task DeleteAsync(TUserId userId, CancellationToken cancellationToken = default);
}
