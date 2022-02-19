using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Repositories.Features;
using Replikit.Abstractions.Repositories.Models;

namespace Replikit.Core.GlobalServices;

public interface IGlobalAdapterRepository : IGlobalHasFeatures<AdapterRepositoryFeatures>
{
    /// <inheritdoc cref="IAdapterRepository.GetChannelInfoAsync" />
    Task<ChannelInfo?> GetChannelInfoAsync(GlobalIdentifier identifier, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IAdapterRepository.GetAccountInfoAsync" />
    Task<AccountInfo?> GetAccountInfoAsync(GlobalIdentifier identifier, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IAdapterRepository.ResolveAttachmentUrlAsync" />
    Task<Attachment> ResolveAttachmentUrlAsync(Attachment attachment, CancellationToken cancellationToken = default);
}
