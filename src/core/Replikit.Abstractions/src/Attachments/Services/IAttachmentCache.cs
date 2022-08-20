using Replikit.Abstractions.Attachments.Models;

namespace Replikit.Abstractions.Attachments.Services;

/// <summary>
/// The service that caches and resolves attachments.
/// </summary>
public interface IAttachmentCache
{
    /// <summary>
    /// Saves the attachment to the cache if they are not already cached.
    /// </summary>
    /// <param name="attachments">A collection of attachments to cache.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task SaveAsync(IEnumerable<SentAttachment> attachments, CancellationToken cancellationToken = default);

    /// <summary>
    /// Resolves the attachments from the cache.
    /// </summary>
    /// <param name="attachments">A collection of attachments to resolve.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains a collection of resolved attachments.
    /// </returns>
    public Task<IReadOnlyList<OutAttachment>> ResolveAsync(IEnumerable<OutAttachment> attachments,
        CancellationToken cancellationToken = default);
}
