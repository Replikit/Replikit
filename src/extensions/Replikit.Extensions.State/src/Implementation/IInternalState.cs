namespace Replikit.Extensions.State.Implementation;

internal interface IInternalState : IState
{
    StateStatus Status { get; }
    StateType Type { get; }
    Type ValueType { get; }
    object? RawValue { get; }

    void SetKey(StateKey key);
    void SetValue(object value);

    void ApplyStatusChange();
}
