using Kantaiko.Routing.Context;
using Replikit.Extensions.State.Context;
using Replikit.Extensions.Storage;
using Replikit.Extensions.Storage.Models;

namespace Replikit.Extensions.State.Implementation;

internal class StateManager : IStateManager, IStateTracker, IStateLoader
{
    private List<IInternalState>? _trackedStates;
    private readonly Dictionary<StateKey, IInternalState> _loadedStates = new();

    private readonly IStorage<StateKey, DynamicValue> _storage;
    private readonly IContextAccessor _contextAccessor;

    public StateManager(IStorage<StateKey, DynamicValue> storage, IContextAccessor contextAccessor)
    {
        _storage = storage;
        _contextAccessor = contextAccessor;
    }

    public IEnumerable<IState> LoadedStates => _loadedStates.Values;

    public async Task<IState<TValue>> GetStateAsync<TValue>(StateKey key, CancellationToken cancellationToken = default)
        where TValue : notnull, new()
    {
        key = key with { Type = typeof(TValue) };

        if (_loadedStates.TryGetValue(key, out var existingValue))
        {
            return (IState<TValue>) existingValue;
        }

        var item = await _storage.CreateQuery()
            .Where(x => x.Key == key)
            .Select(x => x.Value)
            .FirstOrDefaultAsync(cancellationToken);

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

    public async Task<IReadOnlyList<IState>> FindStatesAsync(PartialStateKey partialKey,
        CancellationToken cancellationToken = default)
    {
        var query = _storage.CreateQuery();

        if (partialKey.StateType is not null)
        {
            query = query.Where(x => x.Key.StateType == partialKey.StateType);
        }

        if (partialKey.AdapterId is not null)
        {
            query = query.Where(x => x.Key.AdapterId == partialKey.AdapterId);
        }

        if (partialKey.AccountId is not null)
        {
            query = query.Where(x => x.Key.AccountId == partialKey.AccountId);
        }

        if (partialKey.ChannelId is not null)
        {
            query = query.Where(x => x.Key.ChannelId == partialKey.ChannelId);
        }

        if (partialKey.MessageId is not null)
        {
            query = query.Where(x => x.Key.MessageId == partialKey.MessageId);
        }

        if (partialKey.Type is not null)
        {
            query = query.Where(x => x.Key.Type == partialKey.Type);
        }

        var items = await query.ToArrayAsync(cancellationToken);
        var result = new IState[items.Length];

        for (var index = 0; index < items.Length; index++)
        {
            var (key, item) = items[index];

            if (_loadedStates.TryGetValue(key, out var existingState))
            {
                result[index] = existingState;
            }

            var state = item.GetValue(key.Type!) is not { } value
                ? new State(key)
                : new State(key, value);

            _loadedStates[key] = state;
            result[index] = state;
        }

        return result;
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

        var states = await _storage.CreateQuery()
            .Where(x => newStateKeys.Any(k => k == x.Key))
            .ToArrayAsync(cancellationToken);

        foreach (var (key, state) in states)
        {
            var value = state.GetValue(key.Type!);

            if (value is null)
            {
                throw new InvalidOperationException("Unexpected null state value");
            }

            _loadedStates[key].SetValue(value);
        }
    }

    public virtual async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        KeyValuePair<StateKey, DynamicValue?> MapValue(KeyValuePair<StateKey, IInternalState> state)
        {
            var (key, value) = state;

            var newValue = value.Status is StateStatus.HasModifiedValue ? new DynamicValue(value.RawValue) : null;

            return new KeyValuePair<StateKey, DynamicValue?>(key, newValue);
        }

        var changedStates = _loadedStates
            .Where(x => x.Value.Status is StateStatus.HasClearedValue or StateStatus.HasModifiedValue)
            .ToArray();

        var values = changedStates.Select(MapValue);

        await _storage.SetManyAsync(values, cancellationToken);

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

        var stateKeyFactory = _contextAccessor.Context.GetStateKeyFactory() ?? DefaultStateKeyFactory.Instance;

        return stateKeyFactory.CreateStateKey(state.Type, _contextAccessor.Context);
    }
}
