using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Messages.Models.TextTokens;

namespace Replikit.Abstractions.Messages.Services;

/// <summary>
/// Used to parse text into token array.
/// </summary>
public interface ITextTokenizer
{
    /// <summary>
    /// Transforms message and it's text to token list.
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    IReadOnlyList<TextToken> TokenizeMessage(Message message);
}
