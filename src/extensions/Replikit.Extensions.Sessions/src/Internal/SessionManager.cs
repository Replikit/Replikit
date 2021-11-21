using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Extensions.Common.Sessions;
using Replikit.Extensions.Sessions.Services;

namespace Replikit.Extensions.Sessions.Internal;

internal class SessionManager : ISessionManager
{
    private readonly ISessionStorage _storage;
    private readonly IServiceProvider _serviceProvider;

    private readonly List<IInternalSession> _trackedSessions = new();
    private readonly List<IInternalSession> _loadedSessions = new();

    public SessionManager(ISessionStorage storage, IServiceProvider serviceProvider)
    {
        _storage = storage;
        _serviceProvider = serviceProvider;
    }

    public void Track(IInternalSession session)
    {
        if (!_trackedSessions.Contains(session))
            _trackedSessions.Add(session);
    }

    public async Task<TSession> GetSessionAsync<TSession, TModel>(SessionKey sessionKey,
        CancellationToken cancellationToken = default)
        where TSession : ISession<TModel>
        where TModel : class, new()
    {
        var session = _serviceProvider.GetRequiredService<TSession>();
        var internalSession = (IInternalSession) session;

        internalSession.SessionKey = sessionKey;

        await LoadAsync(cancellationToken);

        return session;
    }

    public async Task<TSession> GetSessionAsync<TSession, TModel>(CancellationToken cancellationToken = default)
        where TSession : ISession<TModel> where TModel : class, new()
    {
        var session = _serviceProvider.GetRequiredService<TSession>();

        await LoadAsync(cancellationToken);

        return session;
    }

    public async Task LoadAsync(CancellationToken cancellationToken = default)
    {
        foreach (var trackedSession in _trackedSessions)
        {
            var sessionKey = trackedSession.SessionKey ?? await trackedSession.CreateSessionKeyAsync();

            var value = await _storage.GetAsync(sessionKey, trackedSession.ValueType, cancellationToken);
            value ??= Activator.CreateInstance(trackedSession.ValueType);

            trackedSession.Value = value!;
        }

        _loadedSessions.AddRange(_trackedSessions);
        _trackedSessions.Clear();
    }

    public async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        foreach (var loadedSession in _loadedSessions)
        {
            Debug.Assert(loadedSession.SessionKey is not null);
            await _storage.SetAsync(loadedSession.SessionKey, loadedSession.Value, cancellationToken);
        }
    }
}
