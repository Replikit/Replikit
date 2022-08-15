using Replikit.Abstractions.Messages.Models.TextTokens;

namespace Replikit.Abstractions.Messages.Services;

/// <summary>
/// Used to format text from token array.
/// </summary>
public interface ITextFormatter
{
    /// <summary>
    /// Formats list of text tokens to string using adapter formatting rules.
    /// Returns empty string if token list is null.
    /// </summary>
    /// <param name="tokens"></param>
    /// <returns></returns>
    string FormatText(IReadOnlyList<TextToken> tokens);
}
