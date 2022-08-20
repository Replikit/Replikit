using Replikit.Abstractions.Common.Exceptions;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Resources;

namespace Replikit.Abstractions.Adapters.Exceptions;

/// <summary>
/// Represents an error occurred when adapter is not found.
/// </summary>
public class AdapterNotFoundException : ReplikitException
{
    /// <summary>
    /// Creates a new instance of <see cref="AdapterNotFoundException"/>.
    /// </summary>
    /// <param name="botId">An identifier of the bot for which the adapter was not found.</param>
    public AdapterNotFoundException(BotIdentifier botId) : base(string.Format(Strings.AdapterNotFound, botId)) { }
}
