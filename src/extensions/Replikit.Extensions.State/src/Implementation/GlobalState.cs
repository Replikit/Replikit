using Replikit.Core.Abstractions.State;

namespace Replikit.Extensions.State.Implementation;

internal class GlobalState<TValue> : State<TValue>, IGlobalState<TValue> where TValue : notnull, new()
{
    public GlobalState(IStateTracker stateTracker) : base(stateTracker) { }

    protected override StateKind Kind => StateKind.GlobalState;
}
