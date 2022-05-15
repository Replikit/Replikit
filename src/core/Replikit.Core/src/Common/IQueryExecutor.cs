namespace Replikit.Core.Common;

public interface IQueryExecutor
{
    Task<IReadOnlyList<TItem>> ToReadOnlyListAsync<TItem>(
        IQueryable<TItem> queryable,
        CancellationToken cancellationToken = default
    );

    Task<TItem?> FirstOrDefaultAsync<TItem>(
        IQueryable<TItem> queryable,
        CancellationToken cancellationToken = default
    );
}
