namespace Replikit.Abstractions.Messages.Models.TextTokens;

public class TextTokenList : List<TextToken>, ITextTokenList
{
    public TextTokenList() { }
    public TextTokenList(IEnumerable<TextToken> collection) : base(collection) { }
    public TextTokenList(int capacity) : base(capacity) { }

    public static implicit operator TextTokenList(TextToken token)
    {
        return new TextTokenList { token };
    }

    public static implicit operator TextTokenList(string text)
    {
        return new TextTokenList { text };
    }
}
