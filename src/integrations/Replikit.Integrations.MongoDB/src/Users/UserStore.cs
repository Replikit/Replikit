using MongoDB.Bson;
using MongoDB.Driver;
using Replikit.Abstractions.Common.Models;
using Replikit.Core.Abstractions.Users;
using Replikit.Integrations.MongoDB.Internal;

namespace Replikit.Integrations.MongoDB.Users;

internal class UserStore<TUser, TUserId> : IUserStore<TUser, TUserId> where TUser : ReplikitUser<TUserId>
{
    private readonly UserDbContext<TUser, TUserId> _dbContext;

    public UserStore(UserDbContext<TUser, TUserId> dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TUser?> FindByIdAsync(TUserId id, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(id);

        return await _dbContext.Users.Find(new BsonDocument("_id", BsonValue.Create(id)))
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<TUser?> FindByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(username);

        return await _dbContext.Users.Find(x => x.Username == username)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<TUser?> FindByAccountIdAsync(GlobalIdentifier accountId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.Find(x => x.AccountIds.Contains(accountId))
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<TUser> AddAsync(TUser user, CancellationToken cancellationToken = default)
    {
        await _dbContext.Users.InsertOneAsync(user, cancellationToken: cancellationToken);

        return user;
    }

    public async Task<TUser> UpdateAsync(TUser user, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(user);

        if (EqualityComparer<TUserId>.Default.Equals(user.Id, default))
        {
            throw new InvalidOperationException("Cannot update user without valid identifier");
        }

        await _dbContext.Users.ReplaceOneAsync(
            new BsonDocument("_id", BsonValue.Create(user.Id)),
            user,
            cancellationToken: cancellationToken
        );

        return user;
    }

    public async Task DeleteAsync(TUserId userId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(userId);

        await _dbContext.Users.DeleteOneAsync(x => userId.Equals(x.Id), cancellationToken: cancellationToken);
    }
}
