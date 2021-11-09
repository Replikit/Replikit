using Replikit.Abstractions.Messages.Models.Tokens;

namespace Replikit.Adapters.Common.Text.Tokenization;

internal record TextModifiersTokenizerRule(string Pattern, TextTokenModifiers Modifiers);
