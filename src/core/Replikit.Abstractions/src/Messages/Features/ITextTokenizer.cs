using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Messages.Models.Tokens;

namespace Replikit.Abstractions.Messages.Features;

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
