namespace Replikit.Extensions.State.Implementation;

internal interface IStateTracker
{
    void Track(IInternalState state);
}
