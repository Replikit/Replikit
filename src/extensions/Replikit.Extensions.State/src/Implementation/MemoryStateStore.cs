using System.Collections.Concurrent;
using Replikit.Core.Abstractions.State;
using Replikit.Core.Common;

namespace Replikit.Extensions.State.Implementation;

internal class MemoryStateStore : IStateStore
{
    private readonly ConcurrentDictionary<StateKey, StateItem> _stateItems = new();

    public IQueryExecutor QueryExecutor => Replikit.Core.Common.QueryExecutor.Instance;

    public IQueryable<StateItem> CreateQuery()
    {
        return _stateItems.Values.AsQueryable();
    }

    public Task SaveManyAsync(IEnumerable<StateItem> items, CancellationToken cancellationToken = default)
    {
        foreach (var stateItem in items)
        {
            _stateItems[stateItem.Key] = stateItem;
        }

        return Task.CompletedTask;
    }
}
