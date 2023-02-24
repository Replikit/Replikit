using Replikit.Abstractions.Common.Exceptions;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Resources;

namespace Replikit.Abstractions.Adapters.Exceptions;

/// <summary>
/// Represents an error occurred when adapter is not found.
/// </summary>
public sealed class AdapterNotFoundException : ReplikitException
{
    internal AdapterNotFoundException(BotIdentifier botId) :
        base(string.Format(Strings.AdapterForBotNotFound, botId)) { }

    internal AdapterNotFoundException(string platformId) : base(
        string.Format(Strings.AdapterForPlatformNotFound, platformId)) { }

    internal AdapterNotFoundException() : base(Strings.NoAdaptersRegistered) { }
}
