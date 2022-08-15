namespace Replikit.Core.Abstractions.State;

/// <summary>
/// The representation of the state item in database.
/// An instance of this type can be fetched from <see cref="IStateStore"/>.
/// This type is not a state itself and cannot be used as a state.
/// But it can be used to create a state in some scope in order not to fetch it from the database again.
/// </summary>
public class StateItem
{
    // ReSharper disable once UnusedMember.Local
    private StateItem() { }

    public StateItem(StateKey key, object? value)
    {
        Key = key;
        // ReSharper disable once VirtualMemberCallInConstructor
        Value = value;
    }

    public StateItem(StateKey key)
    {
        Key = key;
    }

    public virtual StateKey Key { get; private set; } = null!;
    public virtual object? Value { get; private set; }
}

/// <summary>
/// The representation of the state item in database.
/// An instance of this type can be fetched from <see cref="IStateStore"/>.
/// This type is not a state itself and cannot be used as a state.
/// But it can be used to create a state in some scope in order not to fetch it from the database again.
/// </summary>
/// <typeparam name="TValue">The type of the state value.</typeparam>
public class StateItem<TValue> where TValue : class
{
    // ReSharper disable once UnusedMember.Local
    private StateItem() { }

    public StateItem(StateKey key, TValue? value)
    {
        Key = key;
        Value = value;
    }

    public StateItem(StateKey key)
    {
        Key = key;
    }

    public virtual StateKey Key { get; private set; } = null!;
    public virtual TValue? Value { get; private set; }
}
