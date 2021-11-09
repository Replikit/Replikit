using Replikit.Extensions.Common.Sessions;

namespace Replikit.Extensions.Sessions.Services;

public interface ISessionManager
{
    Task<TSession> GetSession<TSession, TModel>(SessionKey sessionKey,
        CancellationToken cancellationToken = default)
        where TSession : ISession<TModel>
        where TModel : class, new();

    Task<TSession> GetSession<TSession, TModel>(CancellationToken cancellationToken = default)
        where TSession : ISession<TModel>
        where TModel : class, new();

    Task Load(CancellationToken cancellationToken = default);
    Task Save(CancellationToken cancellationToken = default);
}
