using Replikit.Core.Abstractions.State;

namespace Replikit.Extensions.State;

public interface IState
{
    StateKey Key { get; }
    object Value { get; }

    bool HasValue { get; }
    void Clear();
}

public interface IState<TValue> : IState where TValue : notnull, new()
{
    new TValue Value { get; }

    object IState.Value => Value;
}
