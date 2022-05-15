using Replikit.Core.Abstractions.State;

namespace Replikit.Extensions.State.Implementation;

internal class State<TValue> : StateBase, IState<TValue> where TValue : notnull, new()
{
    public State(IStateTracker stateTracker) : base(stateTracker) { }

    internal State(StateKey key, TValue value) : base(key, value) { }

    internal State(StateKey key) : base(key) { }

    public new TValue Value => (TValue) base.Value;
    protected override Type ValueType => typeof(TValue);
}

internal class State : StateBase
{
    internal State(StateKey key, object value) : base(key, value)
    {
        ArgumentNullException.ThrowIfNull(key.Type);
        ValueType = key.Type;
    }

    internal State(StateKey key) : base(key)
    {
        ArgumentNullException.ThrowIfNull(key.Type);
        ValueType = key.Type;
    }

    protected override Type ValueType { get; }
}
