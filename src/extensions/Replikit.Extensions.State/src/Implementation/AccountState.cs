namespace Replikit.Extensions.State.Implementation;

internal class AccountState<TValue> : State<TValue>, IAccountState<TValue> where TValue : notnull, new()
{
    public AccountState(IStateTracker stateTracker) : base(stateTracker) { }

    protected override StateType Type => StateType.AccountState;
}
