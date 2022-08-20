using Replikit.Abstractions.Common.Utilities;

namespace Replikit.Abstractions.Common.Models;

/// <summary>
/// The identifier of the bot on the platform (messenger or social network).
/// </summary>
/// <param name="PlatformId">An identifier of the platform.</param>
/// <param name="Value">An identifier of the bot itself.</param>
public readonly record struct BotIdentifier(string PlatformId, Identifier Value)
{
    /// <summary>
    /// The identifier of the platform.
    /// </summary>
    public string PlatformId { get; } = Check.NotNull(PlatformId);

    /// <summary>
    /// The identifier of the bot itself.
    /// </summary>
    public Identifier Value { get; } = Check.NotDefault(Value);
}
