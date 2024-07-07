using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Messages.Models.TextTokens;

namespace Replikit.Abstractions.Messages.Services;

/// <summary>
/// Used to parse text into a token array.
/// </summary>
public interface ITextTokenizer
{
    /// <summary>
    /// Transforms the text of the message into the text token collection.
    /// </summary>
    /// <param name="message">A message to parse.</param>
    /// <returns>A collection of text tokens.</returns>
    IReadOnlyList<TextToken> Tokenize(Message message);
}
