using Replikit.Core.Abstractions.State;
using Replikit.Core.Common;
using Replikit.Core.Routing.Context;

namespace Replikit.Extensions.State.Implementation;

internal class StateManager : IStateManager, IStateTracker, IStateLoader, IStateKeyFactoryAcceptor
{
    private List<IInternalState>? _trackedStates;
    private readonly Dictionary<StateKey, IInternalState> _loadedStates = new();

    private IStateKeyFactory _stateKeyFactory;
    private readonly IStateStore _store;

    public StateManager(IStateStore store, IAdapterEventContextAccessor eventContextAccessor)
    {
        _store = store;

        _stateKeyFactory = new DefaultStateKeyFactory(eventContextAccessor);
    }

    public IEnumerable<IState> LoadedStates => _loadedStates.Values;

    public async Task<IReadOnlyList<IState<TValue>>> FindStatesAsync<TValue>(
        QueryBuilder<StateItem<TValue>>? queryBuilder = null,
        CancellationToken cancellationToken = default) where TValue : class, new()
    {
        var stateItems = await _store.FindStateItemsAsync(queryBuilder, cancellationToken);
        var states = new IState<TValue>[stateItems.Count];

        for (var index = 0; index < stateItems.Count; index++)
        {
            var stateItem = stateItems[index];

            if (_loadedStates.TryGetValue(stateItem.Key, out var existingState))
            {
                states[index] = (IState<TValue>) existingState;
                continue;
            }

            states[index] = CreateLoadedState<TValue>(stateItem.Key, stateItem.Value);
        }

        return states;
    }

    public async Task<IReadOnlyList<IState>> FindAllStatesAsync(QueryBuilder<StateItem> queryBuilder,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(queryBuilder);

        var stateItems = await _store.FindAllStateItemsAsync(queryBuilder, cancellationToken);
        var states = new IState[stateItems.Count];

        for (var index = 0; index < stateItems.Count; index++)
        {
            var stateItem = stateItems[index];

            if (_loadedStates.TryGetValue(stateItem.Key, out var existingState))
            {
                states[index] = existingState;
                continue;
            }

            states[index] = CreateLoadedState(stateItem.Key, stateItem.Value);
        }

        return states;
    }

    public async Task<IState<TValue>> GetStateAsync<TValue>(StateKey key, CancellationToken cancellationToken = default)
        where TValue : class, new()
    {
        ArgumentNullException.ThrowIfNull(key);

        key = key with { Type = typeof(TValue) };

        if (_loadedStates.TryGetValue(key, out var existingValue))
        {
            return (IState<TValue>) existingValue;
        }

        var query = _store.CreateQuery()
            .Where(x => x.Key == key);

        var item = await _store.QueryExecutor.FirstOrDefaultAsync(query, cancellationToken);

        return CreateLoadedState<TValue>(key, item?.Value);
    }

    public IState<TValue> LoadState<TValue>(StateItem<TValue> stateItem) where TValue : class, new()
    {
        ArgumentNullException.ThrowIfNull(stateItem);

        return CreateLoadedState<TValue>(stateItem.Key, stateItem.Value);
    }

    private IState CreateLoadedState(StateKey key, object? item)
    {
        State state;

        if (DynamicValueHelper.Deserialize(item, key.Type!) is not { } value)
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

    private IState<TValue> CreateLoadedState<TValue>(StateKey key, object? item) where TValue : notnull, new()
    {
        State<TValue> state;

        if (DynamicValueHelper.Deserialize<TValue>(item) is not { } value)
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

    public void SetKeyFactory(IStateKeyFactory keyFactory)
    {
        ArgumentNullException.ThrowIfNull(keyFactory);

        _stateKeyFactory = keyFactory;
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
            if (_stateKeyFactory.CreateStateKey(trackedState.Kind, trackedState.ValueType) is not { } key)
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
            var value = DynamicValueHelper.Deserialize(stateItem.Value, stateItem.Key.Type!);

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
                ? value.RawValue
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

        await _store.SaveManyAsync(values, cancellationToken);

        foreach (var state in changedStates)
        {
            state.Value.Persist();
        }
    }
}
