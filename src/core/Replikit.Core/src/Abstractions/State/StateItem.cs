using Replikit.Core.Common;

namespace Replikit.Core.Abstractions.State;

public class StateItem
{
    protected StateItem() { }

    public StateItem(StateKey key, DynamicValue? value)
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
    public virtual object? Value { get; set; }

    public DynamicValue? DynamicValue => (DynamicValue?) Value;
}

public class StateItem<TValue> where TValue : class
{
    protected StateItem() { }

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
