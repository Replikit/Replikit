using Replikit.Abstractions.Messages.Models.Tokens;

namespace Replikit.Adapters.Common.Text.Formatting;

public delegate string TokenVisitorHandler<in T>(T token) where T : TextToken;
