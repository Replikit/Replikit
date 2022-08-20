using Replikit.Abstractions.Common.Models;
using Replikit.Core.Abstractions.State;

namespace Replikit.Extensions.State;

public static class StateManagerExtensions
{
    public static Task<IState<TValue>> GetGlobalStateAsync<TValue>(this IStateManager stateManager,
        CancellationToken cancellationToken = default) where TValue : class, new()
    {
        var key = new StateKey(StateKind.GlobalState);

        return stateManager.GetStateAsync<TValue>(key, cancellationToken);
    }

    public static Task<IState<TValue>> GetChannelStateAsync<TValue>(this IStateManager stateManager,
        BotIdentifier botId, Identifier channelId, CancellationToken cancellationToken = default)
        where TValue : class, new()
    {
        var key = new StateKey(
            StateKind.ChannelState,
            botId,
            channelId
        );

        return stateManager.GetStateAsync<TValue>(key, cancellationToken);
    }

    public static Task<IState<TValue>> GetChannelStateAsync<TValue>(this IStateManager stateManager,
        GlobalIdentifier channelId, CancellationToken cancellationToken = default)
        where TValue : class, new()
    {
        return stateManager.GetChannelStateAsync<TValue>(channelId.BotId, channelId, cancellationToken);
    }

    public static Task<IState<TValue>> GetAccountStateAsync<TValue>(this IStateManager stateManager,
        BotIdentifier botId, Identifier accountId, CancellationToken cancellationToken = default)
        where TValue : class, new()
    {
        var key = new StateKey(
            StateKind.AccountState,
            botId,
            AccountId: accountId
        );

        return stateManager.GetStateAsync<TValue>(key, cancellationToken);
    }

    public static Task<IState<TValue>> GetAccountStateAsync<TValue>(this IStateManager stateManager,
        GlobalIdentifier accountId, CancellationToken cancellationToken = default)
        where TValue : class, new()
    {
        return stateManager.GetAccountStateAsync<TValue>(accountId.BotId, accountId, cancellationToken);
    }
}
