namespace Replikit.Extensions.Common.Sessions;

public interface ISessionStorage
{
    Task SetAsync(SessionKey key, object value, CancellationToken cancellationToken = default);
    Task<object?> GetAsync(SessionKey key, Type type, CancellationToken cancellationToken = default);
}
