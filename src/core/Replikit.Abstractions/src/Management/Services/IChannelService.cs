using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Common.Features;
using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Management.Services;

public interface IChannelService : IHasFeatures<ChannelServiceFeatures>
{
    /// <summary>
    /// Changes the title of the specified channel.
    /// </summary>
    /// <param name="channelId"></param>
    /// <param name="title"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task ChangeTitleAsync(Identifier channelId, string title, CancellationToken cancellationToken = default)
        => throw HasFeaturesHelper.CreateUnsupportedException(this, ChannelServiceFeatures.ChangeTitle);

    /// <summary>
    /// Changes the photo of the specified channel.
    /// </summary>
    /// <param name="channelId"></param>
    /// <param name="photo"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    Task ChangePhotoAsync(Identifier channelId, OutAttachment photo, CancellationToken cancellationToken = default)
        => throw HasFeaturesHelper.CreateUnsupportedException(this, ChannelServiceFeatures.ChangePhoto);
}
