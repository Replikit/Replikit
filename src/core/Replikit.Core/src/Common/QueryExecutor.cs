namespace Replikit.Core.Common;

public class QueryExecutor : IQueryExecutor
{
    public virtual Task<IReadOnlyList<TItem>> ToReadOnlyListAsync<TItem>(IQueryable<TItem> queryable,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IReadOnlyList<TItem>>(queryable.ToArray());
    }

    public virtual Task<TItem?> FirstOrDefaultAsync<TItem>(IQueryable<TItem> queryable,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(queryable.FirstOrDefault());
    }
}
