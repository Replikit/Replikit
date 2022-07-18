using Replikit.Core.Abstractions.State;
using Replikit.Extensions.State.Exceptions;
using Replikit.Extensions.State.Implementation;

namespace Replikit.Extensions.Scenes.Internal;

internal class SceneStateKeyFactory : IStateKeyFactory
{
    public StateKey? CreateStateKey(StateType stateType, object context)
    {
        var sceneContext = (SceneContext) context;

        return stateType switch
        {
            StateType.GlobalState => new StateKey(StateType.GlobalState),
            StateType.State => StateKey.FromChannelId(StateType.State, sceneContext.Request.ChannelId),
            StateType.ChannelState => StateKey.FromChannelId(StateType.ChannelState, sceneContext.Request.ChannelId),
            StateType.AccountState => throw new InvalidStateTypeException(stateType, context),
            _ => throw new ArgumentOutOfRangeException(nameof(stateType))
        };
    }

    public static SceneStateKeyFactory Instance { get; } = new();
}
