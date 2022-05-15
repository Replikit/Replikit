using Replikit.Abstractions.Common.Models;
using Replikit.Core.Abstractions.State;
using Replikit.Extensions.State;

namespace Replikit.Extensions.Scenes;

public static class StateManagerExtensions
{
    public static Task<IState<TState>> GetSceneStateAsync<TState>(this IStateManager stateManager,
        AdapterIdentifier adapterId, Identifier channelId, CancellationToken cancellationToken = default)
        where TState : notnull, new()
    {
        var key = new StateKey(
            StateType.State,
            adapterId,
            channelId
        );

        return stateManager.GetStateAsync<TState>(key, cancellationToken);
    }

    public static Task<IState<TState>> GetSceneStateAsync<TState>(this IStateManager stateManager,
        GlobalIdentifier channelId, CancellationToken cancellationToken = default)
        where TState : notnull, new()
    {
        return stateManager.GetSceneStateAsync<TState>(channelId.AdapterId, channelId, cancellationToken);
    }
}
