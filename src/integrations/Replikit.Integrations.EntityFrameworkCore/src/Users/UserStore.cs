using Microsoft.EntityFrameworkCore;
using Replikit.Abstractions.Common.Models;
using Replikit.Core.Abstractions.Users;
using Replikit.Integrations.EntityFrameworkCore.Internal;

namespace Replikit.Integrations.EntityFrameworkCore.Users;

internal class UserStore<TUser, TUserId> : IUserStore<TUser, TUserId> where TUser : ReplikitUser<TUserId>
{
    private readonly IUserDbContext<TUser, TUserId> _dbContext;

    public UserStore(IUserDbContext<TUser, TUserId> dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TUser?> FindByIdAsync(TUserId id, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(id);

        return await _dbContext.Users.FindAsync(new object[] { id }, cancellationToken: cancellationToken);
    }

    public async Task<TUser?> FindByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(username);

        return await _dbContext.Users.Where(x => x.Username == username).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TUser?> FindByAccountIdAsync(GlobalIdentifier accountId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.Where(x => x.AccountIds.Contains(accountId))
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<TUser> AddAsync(TUser user, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(user);

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return user;
    }

    public async Task<TUser> UpdateAsync(TUser user, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(user);

        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return user;
    }

    public async Task DeleteAsync(TUserId userId, CancellationToken cancellationToken = default)
    {
        var user = await FindByIdAsync(userId, cancellationToken);

        if (user is not null)
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
