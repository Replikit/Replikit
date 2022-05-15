using Kantaiko.Routing.Context;
using Replikit.Core.Abstractions.State;

namespace Replikit.Extensions.State.Implementation;

public interface IStateKeyFactory
{
    StateKey? CreateStateKey(StateType stateType, IContext context);
}
