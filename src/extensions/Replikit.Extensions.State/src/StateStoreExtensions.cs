using Replikit.Core.Abstractions.State;
using Replikit.Core.Common;

namespace Replikit.Extensions.State;

public static class StateStoreExtensions
{
    /// <summary>
    /// Finds states of all types using specified query builder and returns their database representations.
    /// </summary>
    public static async Task<IReadOnlyList<StateItem>> FindAllStateItemsAsync(this IStateStore stateStore,
        QueryBuilder<StateItem> queryBuilder,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(stateStore);
        ArgumentNullException.ThrowIfNull(queryBuilder);

        var query = stateStore.CreateQuery();

        query = queryBuilder.Invoke(query);

        return await stateStore.QueryExecutor.ToReadOnlyListAsync(query, cancellationToken);
    }

    /// <summary>
    /// Finds states of specified type using specified query builder and returns their database representations.
    /// </summary>
    public static Task<IReadOnlyList<StateItem<TValue>>> FindStateItemsAsync<TValue>(this IStateStore stateStore,
        QueryBuilder<StateItem<TValue>>? queryBuilder = null,
        CancellationToken cancellationToken = default)
        where TValue : class, new()
    {
        var query = stateStore.CreateQuery()
            .Where(x => x.Key.TypeName == typeof(TValue).AssemblyQualifiedName!)
            .Select(x => new StateItem<TValue>(x.Key, (TValue?) x.Value));

        if (queryBuilder is not null)
        {
            query = queryBuilder.Invoke(query);
        }

        return stateStore.QueryExecutor.ToReadOnlyListAsync(query, cancellationToken: cancellationToken);
    }
}
