using Replikit.Abstractions.Channels.Services;
using Replikit.Abstractions.Common.Models;

namespace Replikit.Core.GlobalServices;

public interface IGlobalChannelService : IGlobalHasFeatures<ChannelServiceFeatures>
{
    /// <inheritdoc cref="IChannelService.ChangeTitleAsync" />
    Task ChangeTitleAsync(GlobalIdentifier channelId, string title, CancellationToken cancellationToken = default);
}
