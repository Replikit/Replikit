using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Common.Features;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Repositories.Models;

namespace Replikit.Abstractions.Repositories.Services;

/// <summary>
/// Adapter repository is responsible for fetching information about entities from underlying API.
/// It also can cache information to reduce API requests and speed up the application.
/// </summary>
public interface IAdapterRepository : IHasFeatures<AdapterRepositoryFeatures>
{
    /// <summary>
    /// Returns the information about channel from the adapter repository.
    /// May be cached.
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ChannelInfo?> GetChannelInfoAsync(Identifier identifier, CancellationToken cancellationToken = default) =>
        throw HasFeaturesHelper.CreateUnsupportedException(this, AdapterRepositoryFeatures.GetChannelInfo);

    /// <summary>
    /// Returns the information about account from the adapter repository.
    /// May be cached.
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>s
    Task<AccountInfo?> GetAccountInfoAsync(Identifier identifier, CancellationToken cancellationToken = default) =>
        throw HasFeaturesHelper.CreateUnsupportedException(this, AdapterRepositoryFeatures.GetAccountInfo);

    /// <summary>
    /// Resolves attachment url from repository and returns new attachment with it.
    /// If attachment already contains url, just returns itself.
    /// </summary>
    /// <param name="attachment"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Attachment> ResolveAttachmentUrlAsync(Attachment attachment, CancellationToken cancellationToken = default) =>
        throw HasFeaturesHelper.CreateUnsupportedException(this, AdapterRepositoryFeatures.ResolveAttachmentUrl);
}
