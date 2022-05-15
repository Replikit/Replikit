using Replikit.Abstractions.Common.Models;
using Replikit.Core.Abstractions.State;

namespace Replikit.Extensions.State;

public static class StateManagerExtensions
{
    public static Task<IState<TValue>> GetGlobalStateAsync<TValue>(this IStateManager stateManager,
        CancellationToken cancellationToken = default) where TValue : notnull, new()
    {
        var key = new StateKey(StateType.GlobalState);

        return stateManager.GetStateAsync<TValue>(key, cancellationToken);
    }

    public static Task<IState<TValue>> GetChannelStateAsync<TValue>(this IStateManager stateManager,
        AdapterIdentifier adapterId, Identifier channelId, CancellationToken cancellationToken = default)
        where TValue : notnull, new()
    {
        var key = new StateKey(
            StateType.ChannelState,
            adapterId,
            channelId
        );

        return stateManager.GetStateAsync<TValue>(key, cancellationToken);
    }

    public static Task<IState<TValue>> GetChannelStateAsync<TValue>(this IStateManager stateManager,
        GlobalIdentifier channelId, CancellationToken cancellationToken = default)
        where TValue : notnull, new()
    {
        return stateManager.GetChannelStateAsync<TValue>(channelId.AdapterId, channelId, cancellationToken);
    }

    public static Task<IState<TValue>> GetAccountStateAsync<TValue>(this IStateManager stateManager,
        AdapterIdentifier adapterId, Identifier accountId, CancellationToken cancellationToken = default)
        where TValue : notnull, new()
    {
        var key = new StateKey(
            StateType.AccountState,
            adapterId,
            AccountId: accountId
        );

        return stateManager.GetStateAsync<TValue>(key, cancellationToken);
    }

    public static Task<IState<TValue>> GetAccountStateAsync<TValue>(this IStateManager stateManager,
        GlobalIdentifier accountId, CancellationToken cancellationToken = default)
        where TValue : notnull, new()
    {
        return stateManager.GetAccountStateAsync<TValue>(accountId.AdapterId, accountId, cancellationToken);
    }
}
