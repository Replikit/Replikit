using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Management.Services;

namespace Replikit.Core.GlobalServices;

internal class GlobalChannelService : IGlobalChannelService
{
    private readonly IAdapterCollection _adapterCollection;

    public GlobalChannelService(IAdapterCollection adapterCollection)
    {
        _adapterCollection = adapterCollection;
    }

    public ChannelServiceFeatures GetFeatures(AdapterIdentifier adapterId)
    {
        return _adapterCollection.ResolveRequired(adapterId).ChannelService.Features;
    }

    private IChannelService ResolveChannelService(GlobalIdentifier channelId)
    {
        return _adapterCollection.ResolveRequired(channelId).ChannelService;
    }

    public Task ChangeTitleAsync(GlobalIdentifier channelId, string title,
        CancellationToken cancellationToken = default)
    {
        return ResolveChannelService(channelId).ChangeTitleAsync(channelId, title, cancellationToken);
    }

    public Task ChangePhotoAsync(GlobalIdentifier channelId, PhotoAttachment photo,
        CancellationToken cancellationToken = default)
    {
        return ResolveChannelService(channelId).ChangePhotoAsync(channelId, photo, cancellationToken);
    }
}
