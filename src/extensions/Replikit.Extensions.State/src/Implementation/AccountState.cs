using Replikit.Core.Abstractions.State;

namespace Replikit.Extensions.State.Implementation;

internal class AccountState<TValue> : State<TValue>, IAccountState<TValue> where TValue : notnull, new()
{
    public AccountState(IStateTracker stateTracker) : base(stateTracker) { }

    protected override StateKind Kind => StateKind.AccountState;
}
