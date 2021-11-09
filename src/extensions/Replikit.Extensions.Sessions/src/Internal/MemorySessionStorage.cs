using System.Collections.Concurrent;
using Replikit.Extensions.Common.Sessions;

namespace Replikit.Extensions.Sessions.Internal;

internal class MemorySessionStorage : ISessionStorage
{
    public const string Name = "memory";

    private readonly ConcurrentDictionary<SessionKey, object> _sessions = new();

    public Task SetAsync(SessionKey key, object value, CancellationToken cancellationToken = default)
    {
        _sessions[key] = value;
        return Task.CompletedTask;
    }

    public Task<object?> GetAsync(SessionKey key, Type type, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_sessions.GetValueOrDefault(key));
    }
}
