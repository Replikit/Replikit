namespace Replikit.Abstractions.Messages.Models.TextTokens;

/// <summary>
/// The list of text tokens.
/// </summary>
public class TextTokenList : List<TextToken>
{
    /// <summary>
    /// Creates an empty <see cref="TextTokenList"/>.
    /// </summary>
    public TextTokenList() { }

    /// <summary>
    /// Creates a new <see cref="TextTokenList"/> from an enumerable of text tokens.
    /// </summary>
    /// <param name="collection">The collection whose elements are copied to the new list.</param>
    public TextTokenList(IEnumerable<TextToken> collection) : base(collection) { }

    /// <summary>
    /// Creates a new <see cref="TextTokenList"/> with the specified capacity.
    /// </summary>
    /// <param name="capacity">The number of elements that the new list can initially store.</param>
    public TextTokenList(int capacity) : base(capacity) { }

    /// <summary>
    /// Converts a TextToken to a <see cref="TextTokenList"/> with a single element.
    /// </summary>
    /// <param name="token">A text token.</param>
    /// <returns>The created <see cref="TextTokenList"/>.</returns>
    public static implicit operator TextTokenList(TextToken token)
    {
        return new TextTokenList { token };
    }

    /// <summary>
    /// Converts a text to a <see cref="TextTokenList"/> with a single element.
    /// </summary>
    /// <param name="text">A text.</param>
    /// <returns>The created <see cref="TextTokenList"/>.</returns>
    public static implicit operator TextTokenList(string text)
    {
        return new TextTokenList { text };
    }
}
