using System.Collections.Concurrent;
using Replikit.Core.Serialization.Dynamic;

namespace Replikit.Core.Sessions.Storage;

/// <summary>
/// The in-memory thread-safe session storage.
/// </summary>
public sealed class MemorySessionStorage : ISessionStorage
{
    private readonly ConcurrentDictionary<string, IReadOnlyDictionary<string, DynamicValue>> _sessions = new();

    public Task<IReadOnlyDictionary<string, DynamicValue>?> GetSessionDataAsync(string sessionId,
        CancellationToken cancellationToken = default)
    {
        var sessionData = _sessions.GetValueOrDefault(sessionId);

        return Task.FromResult(sessionData);
    }

    public Task SetSessionDataAsync(string sessionId, IReadOnlyDictionary<string, DynamicValue> data,
        CancellationToken cancellationToken = default)
    {
        _sessions[sessionId] = data;

        return Task.CompletedTask;
    }

    public Task ClearSessionDataAsync(string sessionId, CancellationToken cancellationToken = default)
    {
        _sessions.TryRemove(sessionId, out _);

        return Task.CompletedTask;
    }
}
