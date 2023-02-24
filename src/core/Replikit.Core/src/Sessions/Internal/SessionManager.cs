using AsyncKeyedLock;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Replikit.Abstractions.Common.Utilities;
using Replikit.Core.Serialization.Dynamic;
using Replikit.Core.Sessions.Storage;

namespace Replikit.Core.Sessions.Internal;

internal class SessionManager : ISessionManager
{
    private readonly AsyncKeyedLocker<string> _locker = new();
    private readonly IMemoryCache _sessionCache = new MemoryCache(new MemoryCacheOptions());

    private readonly ISessionStorage _sessionStorage;
    private readonly IOptions<SessionOptions> _options;

    public SessionManager(ISessionStorage sessionStorage, IOptions<SessionOptions> options)
    {
        _sessionStorage = sessionStorage;
        _options = options;
    }

    public async Task<ISessionLock> AcquireSessionAsync(string key, CancellationToken cancellationToken = default)
    {
        Check.NotNullOrWhiteSpace(key);

        var sessionLock = await _locker.LockAsync(key, cancellationToken);

        if (!_sessionCache.TryGetValue(key, out Session? session))
        {
            var sessionData = await _sessionStorage.GetSessionDataAsync(key, cancellationToken);

            session = sessionData is null ? new Session() : new Session(sessionData);
            CacheSession(key, session);
        }

        async ValueTask Unlock()
        {
            await UpdateSessionAsync(key, session!, cancellationToken);
            sessionLock.Dispose();
        }

        return new SessionLock(session!, Unlock);
    }

    public async Task ApplySessionAsync(string key, Session session, CancellationToken cancellationToken = default)
    {
        Check.NotNullOrWhiteSpace(key);
        Check.NotNull(session);

        if (session.Data.Count == 0)
        {
            return;
        }

        using var sessionLock = await _locker.LockAsync(key, cancellationToken);

        if (_sessionCache.TryGetValue(key, out Session? storedSession))
        {
            var mergedSessionData = new Dictionary<string, DynamicValue>(storedSession!.Data.Concat(session.Data));

            storedSession = new Session(mergedSessionData);
        }
        else
        {
            var sessionData = await _sessionStorage.GetSessionDataAsync(key, cancellationToken);

            if (sessionData is null)
            {
                storedSession = session;
            }
            else
            {
                var mergedSessionData = new Dictionary<string, DynamicValue>(sessionData.Concat(session.Data));

                storedSession = new Session(mergedSessionData);
            }
        }

        CacheSession(key, storedSession);
        await _sessionStorage.SetSessionDataAsync(key, storedSession.Data, cancellationToken);
    }

    private async Task UpdateSessionAsync(string key, Session value, CancellationToken cancellationToken = default)
    {
        if (value.Data.Count == 0)
        {
            await _sessionStorage.ClearSessionDataAsync(key, cancellationToken);
            _sessionCache.Remove(key);
        }
        else
        {
            await _sessionStorage.SetSessionDataAsync(key, value.Data, cancellationToken);
        }
    }

    private void CacheSession(string key, ISession value)
    {
        if (_options.Value.AbsoluteCacheExpiration is not null)
        {
            _sessionCache.Set(key, value, _options.Value.AbsoluteCacheExpiration.Value);
            return;
        }

        if (_options.Value.SlidingCacheExpiration is not null)
        {
            var options = new MemoryCacheEntryOptions { SlidingExpiration = _options.Value.SlidingCacheExpiration };
            _sessionCache.Set(key, value, options);
        }
    }
}
