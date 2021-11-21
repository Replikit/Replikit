using Replikit.Abstractions.Common.Models;
using Replikit.Extensions.Common.Sessions;

namespace Replikit.Extensions.Sessions.Services;

public static class SessionManagerExtensions
{
    public static Task<IAccountSession<T>> GetAccountSessionAsync<T>(this ISessionManager sessionManager,
        AdapterIdentifier adapterId, Identifier accountId, CancellationToken cancellationToken = default)
        where T : class, new()
    {
        return sessionManager.GetSessionAsync<IAccountSession<T>, T>(new SessionKey(
            typeof(T),
            SessionType.Account,
            adapterId, accountId: accountId), cancellationToken);
    }

    public static Task<IChannelSession<T>> GetChannelSessionAsync<T>(this ISessionManager sessionManager,
        AdapterIdentifier adapterId, Identifier channelId, CancellationToken cancellationToken = default)
        where T : class, new()
    {
        return sessionManager.GetSessionAsync<IChannelSession<T>, T>(new SessionKey(
            typeof(T),
            SessionType.Channel,
            adapterId, channelId), cancellationToken);
    }

    public static Task<IGlobalSession<T>> GetGlobalSessionAsync<T>(this ISessionManager sessionManager,
        CancellationToken cancellationToken = default)
        where T : class, new()
    {
        return sessionManager.GetSessionAsync<IGlobalSession<T>, T>(
            new SessionKey(typeof(T), SessionType.Global),
            cancellationToken);
    }

    public static Task<IAdapterSession<T>> GetAdapterSessionAsync<T>(this ISessionManager sessionManager,
        AdapterIdentifier adapterId, CancellationToken cancellationToken = default) where T : class, new()
    {
        return sessionManager.GetSessionAsync<IAdapterSession<T>, T>(new SessionKey(
            typeof(T),
            SessionType.Adapter,
            adapterId), cancellationToken);
    }

    public static Task<IUserSession<T>> GetUserSessionAsync<T>(this ISessionManager sessionManager, long userId,
        CancellationToken cancellationToken = default)
        where T : class, new()
    {
        return sessionManager.GetSessionAsync<IUserSession<T>, T>(new SessionKey(
            typeof(T),
            SessionType.User,
            userId: userId), cancellationToken);
    }

    public static Task<IAccountSession<T>> GetAccountSessionAsync<T>(this ISessionManager sessionManager,
        CancellationToken cancellationToken = default) where T : class, new()
    {
        return sessionManager.GetSessionAsync<IAccountSession<T>, T>(cancellationToken);
    }

    public static Task<IChannelSession<T>> GetChannelSessionAsync<T>(this ISessionManager sessionManager,
        CancellationToken cancellationToken = default) where T : class, new()
    {
        return sessionManager.GetSessionAsync<IChannelSession<T>, T>(cancellationToken);
    }

    public static Task<IAdapterSession<T>> GetAdapterSessionAsync<T>(this ISessionManager sessionManager,
        CancellationToken cancellationToken = default) where T : class, new()
    {
        return sessionManager.GetSessionAsync<IAdapterSession<T>, T>(cancellationToken);
    }

    public static Task<IUserSession<T>> GetUserSessionAsync<T>(this ISessionManager sessionManager,
        CancellationToken cancellationToken = default) where T : class, new()
    {
        return sessionManager.GetSessionAsync<IUserSession<T>, T>(cancellationToken);
    }
}
