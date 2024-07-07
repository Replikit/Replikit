using Replikit.Abstractions.Messages.Models.TextTokens;

namespace Replikit.Abstractions.Messages.Models;

/// <summary>
/// The list of text tokens.
/// </summary>
public class OutMessageText : List<TextToken>
{
    /// <summary>
    /// Creates an empty <see cref="OutMessageText"/>.
    /// </summary>
    public OutMessageText() { }

    /// <summary>
    /// Creates a new <see cref="OutMessageText"/> from an enumerable of text tokens.
    /// </summary>
    /// <param name="collection">The collection whose elements are copied to the new list.</param>
    public OutMessageText(IEnumerable<TextToken> collection) : base(collection) { }

    /// <summary>
    /// Creates a new <see cref="OutMessageText"/> from two text tokens.
    /// </summary>
    /// <param name="first">The first text token.</param>
    /// <param name="second">The second text token.</param>
    public OutMessageText(TextToken first, TextToken second) : base(2)
    {
        Add(first);
        Add(second);
    }

    /// <summary>
    /// Creates a new <see cref="OutMessageText"/> with the specified capacity.
    /// </summary>
    /// <param name="capacity">The number of elements that the new list can initially store.</param>
    public OutMessageText(int capacity) : base(capacity) { }

    /// <summary>
    /// Adds a list of text tokens to the end of the token list.
    /// </summary>
    /// <param name="tokens">A list of text tokens.</param>
    public void Add(IEnumerable<TextToken> tokens)
    {
        AddRange(tokens);
    }

    /// <summary>
    /// Adds a text token to the end of the token list.
    /// </summary>
    /// <param name="textToken">A text token.</param>
    public new void Add(TextToken textToken)
    {
        base.Add(textToken);
    }

    /// <summary>
    /// Adds all tokens from another <see cref="OutMessageText"/> to the end of the token list.
    /// </summary>
    /// <param name="text">The <see cref="OutMessageText"/> to add tokens from.</param>
    public void Add(OutMessageText text)
    {
        AddRange(text);
    }

    /// <summary>
    /// Flattens the <see cref="OutMessageText"/> into a list of text tokens and adds them to the end of the token list.
    /// </summary>
    /// <param name="texts">The list of <see cref="OutMessageText"/>.</param>
    public void Add(IEnumerable<OutMessageText> texts)
    {
        foreach (var text in texts)
        {
            Add(text);
        }
    }

    /// <summary>
    /// Converts a TextToken to a <see cref="OutMessageText"/> with a single element.
    /// </summary>
    /// <param name="token">A text token.</param>
    /// <returns>The created <see cref="OutMessageText"/>.</returns>
    public static implicit operator OutMessageText(TextToken token)
    {
        return [token];
    }

    /// <summary>
    /// Converts a text to a <see cref="OutMessageText"/> with a single element.
    /// </summary>
    /// <param name="text">A text.</param>
    /// <returns>The created <see cref="OutMessageText"/>.</returns>
    public static implicit operator OutMessageText(string text)
    {
        return [text];
    }

    /// <summary>
    /// Converts a list of text tokens to a <see cref="OutMessageText"/>.
    /// </summary>
    /// <param name="left">A list of text tokens.</param>
    /// <param name="right">A list of text tokens.</param>
    /// <returns>The created <see cref="OutMessageText"/>.</returns>
    public static OutMessageText operator +(OutMessageText left, OutMessageText right)
    {
        var result = new OutMessageText(left.Count + right.Count);
        result.AddRange(left);
        result.AddRange(right);
        return result;
    }
}
