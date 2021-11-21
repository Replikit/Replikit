using Replikit.Extensions.Common.Sessions;

namespace Replikit.Extensions.Sessions.Services;

public interface ISessionManager
{
    Task<TSession> GetSessionAsync<TSession, TModel>(SessionKey sessionKey,
        CancellationToken cancellationToken = default)
        where TSession : ISession<TModel>
        where TModel : class, new();

    Task<TSession> GetSessionAsync<TSession, TModel>(CancellationToken cancellationToken = default)
        where TSession : ISession<TModel>
        where TModel : class, new();

    Task LoadAsync(CancellationToken cancellationToken = default);
    Task SaveAsync(CancellationToken cancellationToken = default);
}
