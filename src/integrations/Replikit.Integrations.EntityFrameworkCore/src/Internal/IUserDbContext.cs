using Microsoft.EntityFrameworkCore;
using Replikit.Core.Abstractions.Users;

namespace Replikit.Integrations.EntityFrameworkCore.Internal;

public interface IUserDbContext<TUser, TUserId> : IDbContext where TUser : ReplikitUser<TUserId>
{
    DbSet<TUser> Users { get; }
}
