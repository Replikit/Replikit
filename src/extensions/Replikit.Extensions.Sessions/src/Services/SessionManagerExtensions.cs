using Replikit.Abstractions.Common.Models;
using Replikit.Extensions.Common.Sessions;

namespace Replikit.Extensions.Sessions.Services;

public static class SessionManagerExtensions
{
    public static Task<IAccountSession<T>> GetAccountSession<T>(this ISessionManager sessionManager,
        AdapterIdentifier adapterId, Identifier accountId, CancellationToken cancellationToken = default)
        where T : class, new()
    {
        return sessionManager.GetSession<IAccountSession<T>, T>(new SessionKey(
            typeof(T),
            SessionType.Account,
            adapterId, accountId: accountId), cancellationToken);
    }

    public static Task<IChannelSession<T>> GetChannelSession<T>(this ISessionManager sessionManager,
        AdapterIdentifier adapterId, Identifier channelId, CancellationToken cancellationToken = default)
        where T : class, new()
    {
        return sessionManager.GetSession<IChannelSession<T>, T>(new SessionKey(
            typeof(T),
            SessionType.Channel,
            adapterId, channelId), cancellationToken);
    }

    public static Task<IMemberSession<T>> GetMemberSession<T>(this ISessionManager sessionManager,
        AdapterIdentifier adapterId, Identifier channelId, Identifier accountId,
        CancellationToken cancellationToken = default) where T : class, new()
    {
        return sessionManager.GetSession<IMemberSession<T>, T>(new SessionKey(
            typeof(T),
            SessionType.Member,
            adapterId, channelId, accountId), cancellationToken);
    }

    public static Task<IGlobalSession<T>> GetGlobalSession<T>(this ISessionManager sessionManager,
        CancellationToken cancellationToken = default)
        where T : class, new()
    {
        return sessionManager.GetSession<IGlobalSession<T>, T>(
            new SessionKey(typeof(T), SessionType.Global),
            cancellationToken);
    }

    public static Task<IAdapterSession<T>> GetAdapterSession<T>(this ISessionManager sessionManager,
        AdapterIdentifier adapterId, CancellationToken cancellationToken = default) where T : class, new()
    {
        return sessionManager.GetSession<IAdapterSession<T>, T>(new SessionKey(
            typeof(T),
            SessionType.Adapter,
            adapterId), cancellationToken);
    }

    public static Task<IUserSession<T>> GetUserSession<T>(this ISessionManager sessionManager, long userId,
        CancellationToken cancellationToken = default)
        where T : class, new()
    {
        return sessionManager.GetSession<IUserSession<T>, T>(new SessionKey(
            typeof(T),
            SessionType.User,
            userId: userId), cancellationToken);
    }

    public static Task<IAccountSession<T>> GetAccountSession<T>(this ISessionManager sessionManager,
        CancellationToken cancellationToken = default) where T : class, new()
    {
        return sessionManager.GetSession<IAccountSession<T>, T>(cancellationToken);
    }

    public static Task<IChannelSession<T>> GetChannelSession<T>(this ISessionManager sessionManager,
        CancellationToken cancellationToken = default) where T : class, new()
    {
        return sessionManager.GetSession<IChannelSession<T>, T>(cancellationToken);
    }

    public static Task<IMemberSession<T>> GetMemberSession<T>(this ISessionManager sessionManager,
        CancellationToken cancellationToken = default) where T : class, new()
    {
        return sessionManager.GetSession<IMemberSession<T>, T>(cancellationToken);
    }

    public static Task<IAdapterSession<T>> GetAdapterSession<T>(this ISessionManager sessionManager,
        CancellationToken cancellationToken = default) where T : class, new()
    {
        return sessionManager.GetSession<IAdapterSession<T>, T>(cancellationToken);
    }

    public static Task<IUserSession<T>> GetUserSession<T>(this ISessionManager sessionManager,
        CancellationToken cancellationToken = default) where T : class, new()
    {
        return sessionManager.GetSession<IUserSession<T>, T>(cancellationToken);
    }
}
