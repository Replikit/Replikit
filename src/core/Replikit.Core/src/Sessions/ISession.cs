namespace Replikit.Core.Sessions;

/// <summary>
/// The object that represents a session.
/// It can be used to store and retrieve data.
/// </summary>
public interface ISession
{
    /// <summary>
    /// Gets a value with the specified key of the specified type.
    /// </summary>
    /// <param name="key">The key of the value.</param>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <returns>
    /// The value with the specified key or the default value of the specified type if the value is not in the session.
    /// </returns>
    TValue? GetValue<TValue>(string key);

    /// <summary>
    /// Sets a value with the specified key.
    /// <br/>
    /// If the value is default, it will be removed from the session.
    /// </summary>
    /// <param name="key">The key of the value.</param>
    /// <param name="value">The value to set.</param>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    void SetValue<TValue>(string key, TValue? value);
}
