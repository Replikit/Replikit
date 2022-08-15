using Replikit.Core.Abstractions.State;

namespace Replikit.Extensions.State.Implementation;

internal interface IInternalState : IState
{
    StateStatus Status { get; }
    StateKind Kind { get; }
    Type ValueType { get; }
    object? RawValue { get; }

    void SetKey(StateKey key);
    void SetValue(object value);

    void Persist();
}
