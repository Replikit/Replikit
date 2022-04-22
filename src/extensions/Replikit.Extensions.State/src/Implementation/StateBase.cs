using System.Diagnostics;

namespace Replikit.Extensions.State.Implementation;

internal abstract class StateBase : IInternalState
{
    private object? _value;
    private StateStatus _status;
    private bool _persisted;

    internal StateBase(IStateTracker stateTracker)
    {
        stateTracker.Track(this);

        _status = StateStatus.Empty;
    }

    internal StateBase(StateKey key, object value)
    {
        Key = key;
        _value = value;

        _persisted = true;
        _status = StateStatus.HasValue;
    }

    internal StateBase(StateKey key)
    {
        Key = key;

        _status = StateStatus.Empty;
    }

    public StateKey Key { get; private set; }

    public object Value
    {
        get
        {
            if (_status is StateStatus.Empty)
            {
                _value ??= Activator.CreateInstance(ValueType)!;
                _status = StateStatus.HasModifiedValue;

                return _value;
            }

            if (_status is StateStatus.HasClearedValue)
            {
                _value ??= Activator.CreateInstance(ValueType)!;

                return _value;
            }

            if (_status is StateStatus.HasValue)
            {
                _status = StateStatus.HasModifiedValue;
            }

            Debug.Assert(_value is not null);
            return _value;
        }
    }

    public void Clear()
    {
        _value = default;

        _status = _persisted
            ? StateStatus.HasClearedValue
            : StateStatus.Empty;
    }

    bool IState.HasValue => _status is not StateStatus.Empty;

    protected abstract Type ValueType { get; }

    Type IInternalState.ValueType => ValueType;

    StateStatus IInternalState.Status => _status;

    void IInternalState.SetKey(StateKey key)
    {
        Key = key;
    }

    void IInternalState.ApplyStatusChange()
    {
        _persisted = true;

        switch (_status)
        {
            case StateStatus.HasClearedValue:
            {
                _status = StateStatus.Empty;
                return;
            }
            case StateStatus.HasModifiedValue:
            {
                _status = StateStatus.HasValue;
                return;
            }
        }

        throw new InvalidOperationException("Unexpected state status");
    }

    void IInternalState.SetValue(object value)
    {
        if (_status is not StateStatus.Empty) return;

        _value = value;
        _persisted = true;
        _status = StateStatus.HasValue;
    }

    object? IInternalState.RawValue => _value;

    protected virtual StateType Type => StateType.State;

    StateType IInternalState.Type => Type;
}
