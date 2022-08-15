using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Repositories.Models;
using Replikit.Abstractions.Repositories.Services;

namespace Replikit.Core.GlobalServices;

public interface IGlobalAdapterRepository : IGlobalHasFeatures<AdapterRepositoryFeatures>
{
    /// <inheritdoc cref="IAdapterRepository.GetChannelInfoAsync" />
    Task<ChannelInfo?> GetChannelInfoAsync(GlobalIdentifier channelId, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IAdapterRepository.GetAccountInfoAsync" />
    Task<AccountInfo?> GetAccountInfoAsync(GlobalIdentifier accountId, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IAdapterRepository.ResolveAttachmentUrlAsync" />
    Task<Attachment> ResolveAttachmentUrlAsync(Attachment attachment, CancellationToken cancellationToken = default);
}
