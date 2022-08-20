using Replikit.Abstractions.Common.Utilities;

namespace Replikit.Abstractions.Adapters;

/// <summary>
/// Represents an information about the adapter.
/// </summary>
/// <param name="Type">A type of the adapter.</param>
/// <param name="DisplayName">A display name of the adapter.</param>
public record AdapterInfo(string Type, string DisplayName)
{
    /// <summary>
    /// The type of the adapter.
    /// <br/>
    /// For example: "telegram" or "telegram:tdlib"
    /// </summary>
    public string Type { get; } = Check.NotNullOrWhiteSpace(Type);

    /// <summary>
    /// The display name of the adapter.
    /// <br/>
    /// For example: "Telegram" or "Telegram (TDLib)"
    /// </summary>
    public string DisplayName { get; } = Check.NotNullOrWhiteSpace(DisplayName);
}
