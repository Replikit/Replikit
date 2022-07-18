using Kantaiko.Routing.Context;
using Replikit.Core.Abstractions.State;
using Replikit.Core.Common;

namespace Replikit.Extensions.State.Implementation;

internal class StateManager : IStateManager, IStateTracker, IStateLoader
{
    private List<IInternalState>? _trackedStates;
    private readonly Dictionary<StateKey, IInternalState> _loadedStates = new();

    private readonly IStateStore _store;
    private readonly IContextAccessor _contextAccessor;

    public StateManager(IStateStore store, IContextAccessor contextAccessor)
    {
        _store = store;
        _contextAccessor = contextAccessor;
    }

    public IEnumerable<IState> LoadedStates => _loadedStates.Values;

    public async Task<IReadOnlyList<IState<TValue>>> FindStatesAsync<TValue>(
        QueryBuilder<StateItem<TValue>>? queryBuilder = null,
        CancellationToken cancellationToken = default) where TValue : class, new()
    {
        var query = _store.CreateQuery()
            .Where(x => x.Key.TypeName == typeof(TValue).AssemblyQualifiedName!)
            .Select(x => new StateItem<TValue>(x.Key, (TValue?) x.Value));

        if (queryBuilder is not null)
        {
            query = queryBuilder.Invoke(query);
        }

        var stateValues = await _store.QueryExecutor.ToReadOnlyListAsync(query, cancellationToken: cancellationToken);
        var states = new IState<TValue>[stateValues.Count];

        for (var index = 0; index < stateValues.Count; index++)
        {
            var stateItem = stateValues[index];

            if (_loadedStates.TryGetValue(stateItem.Key, out var existingState))
            {
                states[index] = (IState<TValue>) existingState;
                continue;
            }

            states[index] = CreateLoadedState<TValue>(stateItem.Key, new DynamicValue(stateItem.Value));
        }

        return states;
    }

    public async Task<IReadOnlyList<IState>> FindAllStatesAsync(QueryBuilder<StateItem> queryBuilder,
        CancellationToken cancellationToken = default)
    {
        var query = _store.CreateQuery();

        query = queryBuilder.Invoke(query);

        var stateValues = await _store.QueryExecutor.ToReadOnlyListAsync(query, cancellationToken);
        var states = new IState[stateValues.Count];

        for (var index = 0; index < stateValues.Count; index++)
        {
            var stateItem = stateValues[index];

            if (_loadedStates.TryGetValue(stateItem.Key, out var existingState))
            {
                states[index] = existingState;
                continue;
            }

            states[index] = CreateLoadedState(stateItem.Key, stateItem.DynamicValue);
        }

        return states;
    }

    public async Task<IState<TValue>> GetStateAsync<TValue>(StateKey key, CancellationToken cancellationToken = default)
        where TValue : notnull, new()
    {
        key = key with { Type = typeof(TValue) };

        if (_loadedStates.TryGetValue(key, out var existingValue))
        {
            return (IState<TValue>) existingValue;
        }

        var query = _store.CreateQuery()
            .Where(x => x.Key == key);

        var item = await _store.QueryExecutor.FirstOrDefaultAsync(query, cancellationToken);

        return CreateLoadedState<TValue>(key, item?.DynamicValue);
    }

    private IState CreateLoadedState(StateKey key, DynamicValue? item)
    {
        State state;

        if (item?.GetValue(key.Type!) is not { } value)
        {
            state = new State(key);
        }
        else
        {
            state = new State(key, value);
        }

        _loadedStates[key] = state;

        return state;
    }

    private IState<TValue> CreateLoadedState<TValue>(StateKey key, DynamicValue? item) where TValue : notnull, new()
    {
        State<TValue> state;

        if (item is null || item.GetValue<TValue>() is not { } value)
        {
            state = new State<TValue>(key);
        }
        else
        {
            state = new State<TValue>(key, value);
        }

        _loadedStates[key] = state;

        return state;
    }

    public void Track(IInternalState state)
    {
        _trackedStates ??= new List<IInternalState>();
        _trackedStates.Add(state);
    }

    public virtual async Task LoadAsync(CancellationToken cancellationToken = default)
    {
        if (_trackedStates is not { Count: > 0 })
        {
            return;
        }

        List<IInternalState>? remainingTrackedStates = null;
        List<StateKey>? newStateKeys = null;

        foreach (var trackedState in _trackedStates)
        {
            if (CreateStateKey(trackedState) is not { } key)
            {
                remainingTrackedStates ??= new List<IInternalState>();
                remainingTrackedStates.Add(trackedState);

                continue;
            }

            key = key with { Type = trackedState.ValueType };

            if (_loadedStates.ContainsKey(key))
            {
                continue;
            }

            trackedState.SetKey(key);
            _loadedStates[key] = trackedState;

            newStateKeys ??= new List<StateKey>();
            newStateKeys.Add(key);
        }

        _trackedStates = remainingTrackedStates;

        if (newStateKeys is null)
        {
            return;
        }

        var query = _store.CreateQuery()
            .Where(x => newStateKeys.Contains(x.Key));

        var states = await _store.QueryExecutor.ToReadOnlyListAsync(query, cancellationToken);

        foreach (var stateItem in states)
        {
            var value = stateItem.DynamicValue?.GetValue(stateItem.Key.Type!);

            if (value is null)
            {
                throw new InvalidOperationException("Unexpected null state value");
            }

            _loadedStates[stateItem.Key].SetValue(value);
        }
    }

    public virtual async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        StateItem MapValue(KeyValuePair<StateKey, IInternalState> state)
        {
            var (key, value) = state;

            var newValue = value.Status is StateStatus.HasModifiedValue
                ? new DynamicValue(value.RawValue)
                : null;

            return new StateItem(key, newValue);
        }

        var changedStates = _loadedStates
            .Where(x => x.Value.Status is StateStatus.HasClearedValue or StateStatus.HasModifiedValue)
            .ToArray();

        if (changedStates.Length == 0)
        {
            return;
        }

        var values = changedStates.Select(MapValue);

        await _store.SetManyAsync(values, cancellationToken);

        foreach (var state in changedStates)
        {
            state.Value.ApplyStatusChange();
        }
    }

    private StateKey? CreateStateKey(IInternalState state)
    {
        if (_contextAccessor.Context is null)
        {
            throw new InvalidOperationException("State cannot be used outside of context");
        }

        var stateKeyFactory = _contextAccessor.Context is IHasStateKeyFactory hasStateKeyFactory
            ? hasStateKeyFactory.StateKeyFactory
            : DefaultStateKeyFactory.Instance;

        return stateKeyFactory.CreateStateKey(state.Type, _contextAccessor.Context);
    }
}
