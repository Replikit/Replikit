using Replikit.Abstractions.Messages.Models.TextTokens;

namespace Replikit.Adapters.Common.Text.Tokenization;

internal record TextModifiersTokenizerRule(string Pattern, TextTokenModifiers Modifiers);
