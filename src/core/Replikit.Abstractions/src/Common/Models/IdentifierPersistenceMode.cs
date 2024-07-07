namespace Replikit.Abstractions.Common.Models;

/// <summary>
/// Defines how global identifiers are stored in the persistent storage.
/// </summary>
public enum IdentifierPersistenceMode : byte
{
    /// <summary>
    /// Stores only the value of the <see cref="GlobalIdentifier"/>.
    /// <br/>
    /// If your bot is targeted to a single platform and you don't need to migrate data between platforms,
    /// this mode is recommended.
    /// </summary>
    Single,

    /// <summary>
    /// Stores the value of the <see cref="GlobalIdentifier"/> and the platform identifier.
    /// <br/>
    /// Use this mode if you plan to support multiple platforms, but only one bot per platform.
    /// </summary>
    MultiPlatform,

    /// <summary>
    /// Stores the value of the <see cref="GlobalIdentifier"/>, the platform identifier and the bot account identifier.
    /// <br/>
    /// This mode allows using multiple bots per platform and migrate data between them.
    /// </summary>
    MultiBot
}
