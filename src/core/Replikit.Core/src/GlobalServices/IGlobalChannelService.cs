using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Management.Services;

namespace Replikit.Core.GlobalServices;

public interface IGlobalChannelService : IGlobalHasFeatures<ChannelServiceFeatures>
{
    /// <inheritdoc cref="IChannelService.ChangeTitleAsync" />
    Task ChangeTitleAsync(GlobalIdentifier channelId, string title, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IChannelService.ChangePhotoAsync" />
    Task ChangePhotoAsync(GlobalIdentifier channelId, PhotoAttachment photo,
        CancellationToken cancellationToken = default);
}
