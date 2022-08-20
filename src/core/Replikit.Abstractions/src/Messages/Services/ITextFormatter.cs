using Replikit.Abstractions.Messages.Models.TextTokens;

namespace Replikit.Abstractions.Messages.Services;

/// <summary>
/// The service that can transform a collection of <see cref="TextToken"/>s into a string.
/// </summary>
public interface ITextFormatter
{
    /// <summary>
    /// Formats the collection of text tokens to string using the adapter formatting rules.
    /// </summary>
    /// <param name="textTokens">A collection of text tokens to format.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.The task result contains the formatted string.
    /// </returns>
    ValueTask<string> FormatTextAsync(IReadOnlyCollection<TextToken> textTokens,
        CancellationToken cancellationToken = default);
}
