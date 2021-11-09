using Replikit.Extensions.Common.Sessions;

namespace Replikit.Extensions.Sessions.Internal;

internal abstract class Session<TValue> : ISession<TValue>, IInternalSession where TValue : class, new()
{
    public Session(SessionManager sessionManager)
    {
        sessionManager.Track(this);
    }

    public TValue Value { get; private set; } = default!;

    object IInternalSession.Value
    {
        get => Value;
        set => Value = (TValue) value;
    }

    public Type ValueType => typeof(TValue);

    protected virtual SessionKey CreateSessionKey() => throw new NotImplementedException();

    public SessionKey SessionKey { get; set; } = null!;

    protected virtual Task<SessionKey> CreateSessionKeyAsync()
    {
        return Task.FromResult(CreateSessionKey());
    }

    async Task<SessionKey> IInternalSession.CreateSessionKeyAsync() => SessionKey = await CreateSessionKeyAsync();
}
