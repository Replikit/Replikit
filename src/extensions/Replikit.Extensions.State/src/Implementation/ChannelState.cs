using Replikit.Core.Abstractions.State;

namespace Replikit.Extensions.State.Implementation;

internal class ChannelState<TValue> : State<TValue>, IChannelState<TValue> where TValue : notnull, new()
{
    public ChannelState(IStateTracker stateTracker) : base(stateTracker) { }

    protected override StateKind Kind => StateKind.ChannelState;
}
