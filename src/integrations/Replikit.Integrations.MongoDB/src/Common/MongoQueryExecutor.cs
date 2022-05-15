using System.Diagnostics;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Replikit.Core.Common;

namespace Replikit.Integrations.MongoDB.Common;

internal class MongoQueryExecutor : IQueryExecutor
{
    public static MongoQueryExecutor Instance { get; } = new();

    public async Task<IReadOnlyList<TItem>> ToReadOnlyListAsync<TItem>(IQueryable<TItem> queryable,
        CancellationToken cancellationToken = default)
    {
        return await GetMongoQueryable(queryable).ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<TItem?> FirstOrDefaultAsync<TItem>(IQueryable<TItem> queryable,
        CancellationToken cancellationToken = default)
    {
        return await GetMongoQueryable(queryable).FirstOrDefaultAsync(cancellationToken);
    }

    [DebuggerStepThrough]
    private static IMongoQueryable<TItem> GetMongoQueryable<TItem>(IQueryable<TItem> queryable)
    {
        ArgumentNullException.ThrowIfNull(queryable);

        if (queryable is IMongoQueryable<TItem> mongoQueryable)
        {
            return mongoQueryable;
        }

        throw new InvalidOperationException("MongoQueryExecutor can only execute IMongoQueryable");
    }
}
