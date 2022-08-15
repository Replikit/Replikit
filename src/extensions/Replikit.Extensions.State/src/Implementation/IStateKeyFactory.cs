using Replikit.Core.Abstractions.State;

namespace Replikit.Extensions.State.Implementation;

public interface IStateKeyFactory
{
    StateKey? CreateStateKey(StateKind stateKind, Type type);
}
