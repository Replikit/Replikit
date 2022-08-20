using Replikit.Abstractions.Messages.Models.TextTokens;

namespace Replikit.Adapters.Common.Text.Formatting;

public delegate ValueTask<string> AsyncTokenVisitorHandler<in T>(T token, CancellationToken cancellationToken)
    where T : TextToken;
