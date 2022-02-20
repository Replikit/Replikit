using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Management.Features;

namespace Replikit.Adapters.Common.Features.Internal;

internal class CommonChannelService : AdapterService, IChannelService
{
    private readonly IChannelService _channelService;

    public CommonChannelService(IAdapter adapter, IChannelService channelService) : base(adapter)
    {
        _channelService = channelService;
    }

    public ChannelServiceFeatures Features => _channelService.Features;

    public Task ChangeTitleAsync(Identifier channelId, string title, CancellationToken cancellationToken = default)
    {
        CheckIdentifier(channelId);

        return _channelService.ChangeTitleAsync(channelId, title, cancellationToken);
    }

    public Task ChangePhotoAsync(Identifier channelId, PhotoAttachment photo,
        CancellationToken cancellationToken = default)
    {
        CheckIdentifier(channelId);

        return _channelService.ChangePhotoAsync(channelId, photo, cancellationToken);
    }
}
