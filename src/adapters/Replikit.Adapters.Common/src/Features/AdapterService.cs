using System.Runtime.CompilerServices;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;
using Replikit.Adapters.Common.Exceptions;

namespace Replikit.Adapters.Common.Features;

public abstract class AdapterService
{
    protected AdapterService(IAdapter adapter)
    {
        Adapter = adapter;
    }

    protected IAdapter Adapter { get; }

    protected GlobalIdentifier CreateGlobalIdentifier(Identifier identifier)
    {
        return identifier is GlobalIdentifier globalIdentifier
            ? globalIdentifier
            : new GlobalIdentifier(identifier, Adapter.Id);
    }

    private void CheckAdapterIdentifier(AdapterIdentifier adapterId)
    {
        if (adapterId != Adapter.Id)
        {
            throw new InvalidAdapterIdentifierException(Adapter.Id, adapterId);
        }
    }

    protected void CheckIdentifier(Identifier identifier,
        [CallerArgumentExpression("identifier")]
        string? paramName = null)
    {
        ArgumentNullException.ThrowIfNull(identifier, paramName);

        if (identifier is GlobalIdentifier globalIdentifier)
        {
            CheckAdapterIdentifier(globalIdentifier.AdapterId);
        }
    }

    protected void CheckIdentifiers(IEnumerable<Identifier> identifiers,
        [CallerArgumentExpression("identifiers")]
        string? paramName = null)
    {
        ArgumentNullException.ThrowIfNull(identifiers, paramName);

        foreach (var identifier in identifiers)
        {
            if (identifier is GlobalIdentifier globalIdentifier)
            {
                CheckAdapterIdentifier(globalIdentifier.AdapterId);
            }
        }
    }

    protected void CheckIdentifier(MessageIdentifier identifier,
        [CallerArgumentExpression("identifier")]
        string? paramName = null)
    {
        ArgumentNullException.ThrowIfNull(identifier, paramName);

        if (identifier is GlobalMessageIdentifier globalIdentifier)
        {
            CheckAdapterIdentifier(globalIdentifier.ChannelId.AdapterId);
        }
    }

    protected void CheckIdentifiers(IEnumerable<MessageIdentifier> identifiers,
        [CallerArgumentExpression("identifiers")]
        string? paramName = null)
    {
        ArgumentNullException.ThrowIfNull(identifiers, paramName);

        foreach (var identifier in identifiers)
        {
            if (identifier is GlobalMessageIdentifier globalIdentifier)
            {
                CheckAdapterIdentifier(globalIdentifier.ChannelId.AdapterId);
            }
        }
    }
}
