using Replikit.Abstractions.Channels.Models;
using Replikit.Abstractions.Common.Exceptions;
using Replikit.Abstractions.Common.Features;
using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Channels.Services;

public interface IChannelService : IHasFeatures<ChannelServiceFeatures>
{
    /// <summary>
    /// Gets the channel with the specified identifier.
    /// </summary>
    /// <param name="channelId">An identifier of the channel.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the channel or <c>null</c> if it was not found.
    /// </returns>
    /// <exception cref="UnsupportedFeatureException">
    /// The method is not supported by the service implementation.
    /// </exception>
    Task<ChannelInfo?> GetAsync(Identifier channelId, CancellationToken cancellationToken = default)
    {
        throw HasFeaturesHelper.CreateUnsupportedException(this, ChannelServiceFeatures.Get);
    }

    /// <summary>
    /// Changes the title of the channel.
    /// </summary>
    /// <param name="channelId">An identifier of the channel.</param>
    /// <param name="title">A new title of the channel.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    /// <exception cref="UnsupportedFeatureException">
    /// The method is not supported by the service implementation.
    /// </exception>
    Task ChangeTitleAsync(Identifier channelId, string title, CancellationToken cancellationToken = default)
    {
        throw HasFeaturesHelper.CreateUnsupportedException(this, ChannelServiceFeatures.ChangeTitle);
    }
}
