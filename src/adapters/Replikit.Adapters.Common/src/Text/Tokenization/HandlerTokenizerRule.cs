using System.Text.RegularExpressions;

namespace Replikit.Adapters.Common.Text.Tokenization;

internal record HandlerTokenizerRule(Regex Pattern, TokenizerHandler Handler);
