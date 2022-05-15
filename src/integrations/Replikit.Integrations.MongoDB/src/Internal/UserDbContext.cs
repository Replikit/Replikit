using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Replikit.Core.Abstractions.Users;
using Replikit.Integrations.MongoDB.Options;

namespace Replikit.Integrations.MongoDB.Internal;

internal class UserDbContext<TUser, TUserId> where TUser : ReplikitUser<TUserId>
{
    public IMongoCollection<TUser> Users { get; }

    public UserDbContext(RootDbContext dbContext, IOptions<ReplikitMongoOptions> options)
    {
        Users = dbContext.Database.GetCollection<TUser>(options.Value.UserCollectionName);
    }
}
