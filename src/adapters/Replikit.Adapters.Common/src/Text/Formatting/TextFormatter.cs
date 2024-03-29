﻿using System.Text;
using Replikit.Abstractions.Messages.Models.TextTokens;
using Replikit.Abstractions.Messages.Services;
using Replikit.Adapters.Common.Exceptions;
using Replikit.Adapters.Common.Extensions;

namespace Replikit.Adapters.Common.Text.Formatting;

public class TextFormatter : ITextFormatter
{
    private readonly Dictionary<Type, AsyncTokenVisitorHandler<TextToken>> _tokenVisitors = new();
    private readonly Dictionary<TextTokenModifiers, ModifiersFormatter> _modifiersFormatters = new();

    public TextFormatter AddVisitor<T>(TokenVisitorHandler<T> handler) where T : TextToken
    {
        _tokenVisitors[typeof(T)] = (token, _) => ValueTask.FromResult(handler((T) token));

        return this;
    }

    public TextFormatter AddVisitor<T>(AsyncTokenVisitorHandler<T> handler) where T : TextToken
    {
        _tokenVisitors[typeof(T)] = (token, cancellationToken) => handler((T) token, cancellationToken);

        return this;
    }

    public TextFormatter AddModifiersFormatter(
        TextTokenModifiers modifiers,
        string openingText,
        string? closingText = null)
    {
        _modifiersFormatters[modifiers] = new ModifiersFormatter(openingText, closingText);
        return this;
    }

    public async ValueTask<string> FormatTextAsync(IReadOnlyCollection<TextToken> tokens,
        CancellationToken cancellationToken = default)
    {
        var result = new StringBuilder();

        foreach (var token in tokens)
        {
            var tokenType = token.GetType();
            if (!_tokenVisitors.TryGetValue(tokenType, out var visitor))
            {
                if (tokenType == typeof(TextToken))
                {
                    if (!string.IsNullOrEmpty(token.Text))
                        result.Append(ApplyModifiersFormatters(token.Text, token));
                    continue;
                }

                throw new TextFormattingException($"TokenVisitor for type {tokenType} not found");
            }

            var content = await visitor(token, cancellationToken);
            result.Append(ApplyModifiersFormatters(content, token));
        }

        return result.ToString();
    }

    private string ApplyModifiersFormatters(string tokenText, TextToken token)
    {
        foreach (var modifier in token.Modifiers.GetFlags())
        {
            if (!_modifiersFormatters.TryGetValue(modifier, out var formatter))
                continue;
            tokenText = formatter.OpeningText + tokenText;
            tokenText += formatter.ClosingText ?? formatter.OpeningText;
        }

        return tokenText;
    }
}
