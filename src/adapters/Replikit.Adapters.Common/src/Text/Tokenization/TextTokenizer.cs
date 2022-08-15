using System.Text.RegularExpressions;
using Replikit.Abstractions.Messages.Models.TextTokens;
using Replikit.Adapters.Common.Extensions;

namespace Replikit.Adapters.Common.Text.Tokenization;

public class TextTokenizer
{
    private readonly List<HandlerTokenizerRule> _handlerTokenizerRules = new();
    private readonly List<TextModifiersTokenizerRule> _textModifiersTokenizerRules = new();

    public TextTokenizer AddRegexpRule(string pattern, TokenizerHandler handler)
    {
        var handlerTokenizerRule = new HandlerTokenizerRule(new Regex(pattern), handler);
        _handlerTokenizerRules.Add(handlerTokenizerRule);
        return this;
    }

    public TextTokenizer AddTextModifiersRule(string pattern, TextTokenModifiers modifiers)
    {
        var textModifiersTokenizerRule = new TextModifiersTokenizerRule(pattern, modifiers);
        _textModifiersTokenizerRules.Add(textModifiersTokenizerRule);
        return this;
    }

    public IReadOnlyList<TextToken> Tokenize(string? text)
    {
        if (string.IsNullOrEmpty(text)) return Array.Empty<TextToken>();
        var tokens = new List<TextToken>();

        var matches = _handlerTokenizerRules.SelectMany(rule =>
        {
            var matchCollection = rule.Pattern.Matches(text);
            return matchCollection.Select(match => new { Rule = rule, Match = match });
        }).ToArray();

        if (matches.Length == 0) return MatchTextModifiersRules(text);

        var lastIndex = 0;
        foreach (var match in matches)
        {
            var plainText = text.Slice(lastIndex, match.Match.Index);
            if (plainText != string.Empty)
                tokens.AddRange(MatchTextModifiersRules(plainText));
            tokens.Add(match.Rule.Handler(match.Match.Groups));
            lastIndex = match.Match.Index + match.Match.Length;
        }

        if (text.Length > lastIndex)
        {
            var plainText = text.Slice(lastIndex, text.Length);
            if (plainText != string.Empty)
                tokens.AddRange(MatchTextModifiersRules(plainText));
        }

        return tokens;
    }

    private IReadOnlyList<TextToken> MatchTextModifiersRules(string text)
    {
        var tokens = new List<TextToken>();
        var modifiers = TextTokenModifiers.None;
        var chars = text.ToArray();

        var buf = "";
        for (var i = 0; i < chars.Length; i++)
        {
            var c = chars[i];
            buf += c;

            if ((i == 0 || c != chars[i - 1]) && (i < chars.Length - 1 && c == chars[i + 1]))
                continue;

            var rule = _textModifiersTokenizerRules.Find(x => buf.EndsWith(x.Pattern));
            if (rule is null)
                continue;

            var plainText = buf.Replace(rule.Pattern, "");
            if (plainText != string.Empty)
            {
                var textToken = new TextToken(plainText, modifiers);
                tokens.Add(textToken);
            }

            if (modifiers.HasFlag(rule.Modifiers))
                modifiers &= ~rule.Modifiers;
            else
                modifiers |= rule.Modifiers;

            buf = "";
        }

        if (buf != string.Empty)
        {
            var textToken = new TextToken(buf);
            tokens.Add(textToken);
        }

        return tokens;
    }
}
