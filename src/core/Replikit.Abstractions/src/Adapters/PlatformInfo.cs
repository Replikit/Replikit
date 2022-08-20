using Replikit.Abstractions.Common.Utilities;

namespace Replikit.Abstractions.Adapters;

/// <summary>
/// Represents an information about the platform.
/// </summary>
/// <param name="Id">An identifier of the platform.</param>
/// <param name="DisplayName">A display name of the platform.</param>
public record PlatformInfo(string Id, string DisplayName)
{
    /// <summary>
    /// The unique identifier of the platform.
    /// <br/>
    /// For example: "discord" or "telegram".
    /// </summary>
    public string Id { get; } = Check.NotNullOrWhiteSpace(Id);

    /// <summary>
    /// The display name of the platform.
    /// <br/>
    /// For example: "Discord" or "Telegram".
    /// </summary>
    public string DisplayName { get; } = Check.NotNullOrWhiteSpace(DisplayName);
}
