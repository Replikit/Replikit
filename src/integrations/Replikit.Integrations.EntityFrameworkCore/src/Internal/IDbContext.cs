using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Replikit.Integrations.EntityFrameworkCore.Internal;

public interface IDbContext
{
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
