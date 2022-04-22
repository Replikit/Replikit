using Kantaiko.Routing.Context;

namespace Replikit.Extensions.State.Implementation;

public interface IStateKeyFactory
{
    StateKey? CreateStateKey(StateType stateType, IContext context);
}
