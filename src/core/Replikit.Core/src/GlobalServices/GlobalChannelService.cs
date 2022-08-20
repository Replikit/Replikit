using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Channels.Services;
using Replikit.Abstractions.Common.Models;

namespace Replikit.Core.GlobalServices;

internal class GlobalChannelService : IGlobalChannelService
{
    private readonly IAdapterCollection _adapterCollection;

    public GlobalChannelService(IAdapterCollection adapterCollection)
    {
        _adapterCollection = adapterCollection;
    }

    public ChannelServiceFeatures GetFeatures(BotIdentifier botId)
    {
        return _adapterCollection.ResolveRequired(botId).ChannelService.Features;
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
}
