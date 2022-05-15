using System.Runtime.CompilerServices;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;

namespace Replikit.Adapters.Common.Services;

public abstract class AdapterService
{
    protected AdapterService(AdapterIdentifier adapterId)
    {
        AdapterId = adapterId;
    }

    protected AdapterIdentifier AdapterId { get; }

    protected GlobalIdentifier CreateGlobalIdentifier(Identifier identifier)
    {
        return new GlobalIdentifier(AdapterId, identifier);
    }

    protected void CheckIdentifier(Identifier identifier,
        [CallerArgumentExpression("identifier")]
        string? paramName = null)
    {
        ArgumentNullException.ThrowIfNull(identifier.Value);
    }

    protected void CheckIdentifiers(IEnumerable<Identifier> identifiers,
        [CallerArgumentExpression("identifiers")]
        string? paramName = null)
    {
        ArgumentNullException.ThrowIfNull(identifiers, paramName);

        foreach (var identifier in identifiers)
        {
            ArgumentNullException.ThrowIfNull(identifier.Value);
        }
    }

    protected void CheckIdentifier(MessageIdentifier identifier,
        [CallerArgumentExpression("identifier")]
        string? paramName = null)
    {
        ArgumentNullException.ThrowIfNull(identifier.Identifiers);
    }

    protected void CheckIdentifiers(IEnumerable<MessageIdentifier> identifiers,
        [CallerArgumentExpression("identifiers")]
        string? paramName = null)
    {
        ArgumentNullException.ThrowIfNull(identifiers, paramName);

        foreach (var identifier in identifiers)
        {
            ArgumentNullException.ThrowIfNull(identifier.Identifiers);

            foreach (var messageId in identifier.Identifiers)
            {
                ArgumentNullException.ThrowIfNull(messageId.Value);
            }
        }
    }
}
