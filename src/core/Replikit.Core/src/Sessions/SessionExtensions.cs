using System.Collections.Concurrent;
using System.Reflection;
using Replikit.Abstractions.Common.Exceptions;
using Replikit.Abstractions.Common.Utilities;
using Replikit.Core.Resources;

namespace Replikit.Core.Sessions;

/// <summary>
/// The extensions for <see cref="ISession"/>.
/// </summary>
public static class SessionExtensions
{
    private static readonly ConcurrentDictionary<Type, string> SessionKeys = new();

    /// <summary>
    /// Get the value by the specified key or throw an exception if the value is not found.
    /// </summary>
    /// <param name="session">The session to get the value from.</param>
    /// <param name="key">The key of the value.</param>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <returns>The value with the specified key.</returns>
    /// <exception cref="ReplikitException">
    /// The value is not found.
    /// </exception>
    public static T GetRequiredValue<T>(this ISession session, string key) where T : notnull
    {
        Check.NotNull(session);
        Check.NotNull(key);

        return session.GetValue<T>(key) ?? throw new ReplikitException(string.Format(Strings.NullSessionValue, key));
    }

    /// <summary>
    /// Wraps the session value with the specified key into <see cref="ISession{T}"/>.
    /// <br/>
    /// The returned object will be bound to the session and will
    /// always provide current value and update it directly in the session.
    /// </summary>
    /// <param name="session">The session to use.</param>
    /// <param name="key">The key of the value.</param>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <returns>The strongly typed session.</returns>
    public static ISession<T> GetSession<T>(this ISession session, string key)
    {
        Check.NotNull(session);
        Check.NotNull(key);

        return new Session<T>(session, key);
    }

    /// <summary>
    /// Wraps the session value with the default key into <see cref="ISession{T}"/>.
    /// <br/>
    /// By default this key will be the full name of the value type
    /// and can be overridden using <see cref="SessionAttribute"/>.
    /// <br/>
    /// The returned object will be bound to the session and will
    /// always provide current value and update it directly in the session.
    /// The
    /// </summary>
    /// <param name="session">The session to use.</param>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <returns>The strongly typed session.</returns>
    public static ISession<T> GetSession<T>(this ISession session)
    {
        Check.NotNull(session);

        var sessionKey = SessionKeys.GetOrAdd(typeof(T), GetSessionKey);
        return new Session<T>(session, sessionKey);
    }

    private static string GetSessionKey(Type sessionType)
    {
        return sessionType.GetCustomAttribute<SessionAttribute>()?.Key ?? sessionType.FullName ?? sessionType.Name;
    }
}
