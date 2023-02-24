using Replikit.Abstractions.Common.Utilities;

namespace Replikit.Core.Sessions;

/// <summary>
/// The attribute that allows to specify the default key for typed session.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class SessionAttribute : Attribute
{
    /// <summary>
    /// Creates a new instance of <see cref="SessionAttribute"/>.
    /// </summary>
    /// <param name="key">The default key for typed session.</param>
    public SessionAttribute(string key)
    {
        Check.NotNull(key);

        Key = key;
    }

    /// <summary>
    /// The default key for typed session.
    /// </summary>
    public string Key { get; }
}
