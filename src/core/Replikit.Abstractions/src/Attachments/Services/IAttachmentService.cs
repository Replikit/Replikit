using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Common.Exceptions;
using Replikit.Abstractions.Common.Features;
using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Attachments.Services;

public interface IAttachmentService : IHasFeatures<AttachmentServiceFeatures>
{
    /// <summary>
    /// Gets the attachment with the specified identifier.
    /// </summary>
    /// <param name="attachmentId">An identifier of the attachment.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the attachment or <c>null</c> if it was not found.
    /// </returns>
    /// <exception cref="UnsupportedFeatureException">
    /// The method is not supported by the service implementation.
    /// </exception>
    Task<Attachment?> GetAsync(Identifier attachmentId, CancellationToken cancellationToken = default)
    {
        throw HasFeaturesHelper.CreateUnsupportedException(this, AttachmentServiceFeatures.Get);
    }

    /// <summary>
    /// Gets the content of the attachment with the specified identifier.
    /// </summary>
    /// <param name="attachmentId">An identifier of the attachment.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the content stream of the attachment or <c>null</c> if attachment was not found.
    /// </returns>
    /// <exception cref="UnsupportedFeatureException">
    /// The method is not supported by the service implementation.
    /// </exception>
    Task<Stream?> GetContentAsync(Identifier attachmentId, CancellationToken cancellationToken = default)
    {
        throw HasFeaturesHelper.CreateUnsupportedException(this, AttachmentServiceFeatures.GetContent);
    }

    /// <summary>
    /// Gets the url of the attachment with the specified identifier.
    /// </summary>
    /// <param name="attachmentId">An identifier of the attachment.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the url of the attachment or <c>null</c> if attachment was not found.
    /// </returns>
    /// <exception cref="UnsupportedFeatureException">
    /// The method is not supported by the service implementation.
    /// </exception>
    Task<Uri?> GetUrlAsync(Identifier attachmentId, CancellationToken cancellationToken = default)
    {
        throw HasFeaturesHelper.CreateUnsupportedException(this, AttachmentServiceFeatures.GetUrl);
    }
}
