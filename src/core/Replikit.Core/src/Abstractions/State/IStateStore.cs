using Replikit.Core.Common;

namespace Replikit.Core.Abstractions.State;

public interface IStateStore
{
    IQueryExecutor QueryExecutor { get; }

    IQueryable<StateItem> CreateQuery();

    Task SetManyAsync(IEnumerable<StateItem> items, CancellationToken cancellationToken = default);
}
