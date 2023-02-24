using Replikit.Abstractions.Common.Utilities;

namespace Replikit.Abstractions.Common.Models;

/// <summary>
/// The identifier of the bot on the platform (messenger or social network).
/// </summary>
/// <param name="PlatformId">The identifier of the platform.</param>
/// <param name="Value">The identifier of the bot itself.</param>
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

    public override string ToString() => $"{PlatformId}:{Value}";

    /// <summary>
    /// Tries to parse the string to <see cref="BotIdentifier"/>.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <param name="result">The result of the parsing.</param>
    /// <returns>True if the string was parsed successfully, false otherwise.</returns>
    public static bool TryParse(string? value, out BotIdentifier result)
    {
        if (string.IsNullOrEmpty(value))
        {
            result = default;
            return false;
        }

        var parts = value.Split(':');

        if (parts.Length != 2)
        {
            result = default;
            return false;
        }

        if (!Identifier.TryParse(parts[1], out var identifier))
        {
            result = default;
            return false;
        }

        result = new BotIdentifier(parts[0], identifier);
        return true;
    }
}
