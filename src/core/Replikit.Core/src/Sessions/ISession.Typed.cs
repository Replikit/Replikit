namespace Replikit.Core.Sessions;

/// <summary>
/// Provides a strongly typed access to the one of the session values.
/// </summary>
/// <typeparam name="TValue">The type of the value.</typeparam>
public interface ISession<TValue>
{
    /// <summary>
    /// The session value.
    /// </summary>
    TValue? Value { get; set; }
}
