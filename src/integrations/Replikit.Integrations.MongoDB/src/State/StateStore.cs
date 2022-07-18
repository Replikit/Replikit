using MongoDB.Driver;
using Replikit.Core.Abstractions.State;
using Replikit.Core.Common;
using Replikit.Integrations.MongoDB.Common;
using Replikit.Integrations.MongoDB.Internal;

namespace Replikit.Integrations.MongoDB.State;

internal class StateStore : IStateStore
{
    private readonly StateDbContext _dbContext;

    public StateStore(StateDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryExecutor QueryExecutor => MongoQueryExecutor.Instance;

    public IQueryable<StateItem> CreateQuery()
    {
        return _dbContext.States.AsQueryable();
    }

    public Task SetManyAsync(IEnumerable<StateItem> items, CancellationToken cancellationToken = default)
    {
        var operations = new List<WriteModel<StateItem>>();

        foreach (var stateItem in items)
        {
            var filter = new ExpressionFilterDefinition<StateItem>(x => x.Key == stateItem.Key);

            if (Equals(stateItem.Value, default))
            {
                operations.Add(new DeleteOneModel<StateItem>(filter));
            }
            else
            {
                operations.Add(new ReplaceOneModel<StateItem>(filter, stateItem) { IsUpsert = true });
            }
        }

        return _dbContext.States.BulkWriteAsync(operations, cancellationToken: cancellationToken);
    }
}
