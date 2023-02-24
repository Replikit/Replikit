using Replikit.Abstractions.Common.Utilities;

namespace Replikit.Core.Sessions;

/// <summary>
/// <inheritdoc cref="ISession{TValue}"/>
/// </summary>
public class Session<TValue> : ISession<TValue>
{
    private readonly ISession _session;
    private readonly string _key;

    /// <summary>
    /// Creates a new instance of <see cref="Session{TValue}"/>
    /// </summary>
    /// <param name="session">The session to use.</param>
    /// <param name="key">The key to retrieve and store value.</param>
    public Session(ISession session, string key)
    {
        Check.NotNull(session);
        Check.NotNull(key);

        _session = session;
        _key = key;
    }

    public TValue? Value
    {
        get => _session.GetValue<TValue>(_key);
        set => _session.SetValue(_key, value);
    }
}
