using Replikit.Core.Abstractions.State;
using Replikit.Core.Common;
using Replikit.Integrations.EntityFrameworkCore.Common;
using Replikit.Integrations.EntityFrameworkCore.Internal;

namespace Replikit.Integrations.EntityFrameworkCore.State;

internal class StateStore : IStateStore
{
    private readonly IStateDbContext _stateDbContext;

    public StateStore(IStateDbContext stateDbContext)
    {
        _stateDbContext = stateDbContext;
    }

    public IQueryExecutor QueryExecutor => EfCoreQueryExecutor.Instance;

    public IQueryable<StateItem> CreateQuery()
    {
        return _stateDbContext.States;
    }

    public Task SetManyAsync(IEnumerable<StateItem> items, CancellationToken cancellationToken = default)
    {
        foreach (var stateItem in items)
        {
            var item = _stateDbContext.States.Local.FirstOrDefault(x => x.Key.Equals(stateItem.Key));

            if (!Equals(stateItem.Value, default))
            {
                if (item is null)
                {
                    _stateDbContext.States.Add(stateItem);
                }
                else
                {
                    item.Value = stateItem.Value;
                    _stateDbContext.States.Update(item);
                }
            }
            else
            {
                if (item is null)
                {
                    _stateDbContext.States.Attach(stateItem);
                }

                _stateDbContext.States.Remove(stateItem);
            }
        }

        return _stateDbContext.SaveChangesAsync(cancellationToken);
    }
}
