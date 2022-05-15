using Microsoft.EntityFrameworkCore;
using Replikit.Core.Common;

namespace Replikit.Integrations.EntityFrameworkCore.Common;

internal class EfCoreQueryExecutor : IQueryExecutor
{
    public static EfCoreQueryExecutor Instance { get; } = new();

    public async Task<IReadOnlyList<TItem>> ToReadOnlyListAsync<TItem>(IQueryable<TItem> queryable,
        CancellationToken cancellationToken = default)
    {
        return await queryable.ToArrayAsync(cancellationToken);
    }

    public Task<TItem?> FirstOrDefaultAsync<TItem>(IQueryable<TItem> queryable,
        CancellationToken cancellationToken = default)
    {
        return queryable.FirstOrDefaultAsync(cancellationToken);
    }
}
