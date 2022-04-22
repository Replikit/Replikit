using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Common.Exceptions;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Repositories.Models;
using Replikit.Abstractions.Repositories.Services;

namespace Replikit.Core.GlobalServices;

internal class GlobalAdapterRepository : IGlobalAdapterRepository
{
    private readonly IAdapterCollection _adapterCollection;

    public GlobalAdapterRepository(IAdapterCollection adapterCollection)
    {
        _adapterCollection = adapterCollection;
    }

    public AdapterRepositoryFeatures GetFeatures(AdapterIdentifier adapterId)
    {
        return _adapterCollection.ResolveRequired(adapterId).Repository.Features;
    }

    private IAdapterRepository GetAdapterRepository(GlobalIdentifier identifier)
    {
        return _adapterCollection.ResolveRequired(identifier).Repository;
    }

    public Task<ChannelInfo?> GetChannelInfoAsync(GlobalIdentifier identifier,
        CancellationToken cancellationToken = default)
    {
        return GetAdapterRepository(identifier).GetChannelInfoAsync(identifier, cancellationToken);
    }

    public Task<AccountInfo?> GetAccountInfoAsync(GlobalIdentifier identifier,
        CancellationToken cancellationToken = default)
    {
        return GetAdapterRepository(identifier).GetAccountInfoAsync(identifier, cancellationToken);
    }

    public Task<Attachment> ResolveAttachmentUrlAsync(Attachment attachment,
        CancellationToken cancellationToken = default)
    {
        if (attachment.Id == default)
        {
            throw new ReplikitException("Attachment must have identifier");
        }

        return GetAdapterRepository(attachment.Id).ResolveAttachmentUrlAsync(attachment, cancellationToken);
    }
}
