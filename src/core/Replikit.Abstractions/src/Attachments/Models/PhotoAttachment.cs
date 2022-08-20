using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Attachments.Models;

/// <summary>
/// The photo attachment.
/// </summary>
public class PhotoAttachment : Attachment
{
    /// <summary>
    /// Creates a new instance of <see cref="PhotoAttachment"/>.
    /// </summary>
    /// <param name="id">An identifier of the attachment.</param>
    /// <param name="sizes">A collection of photo sizes.</param>
    public PhotoAttachment(GlobalIdentifier id, IReadOnlyList<PhotoSize> sizes) : base(id, AttachmentType.Photo)
    {
        Sizes = sizes;
    }

    /// <summary>
    /// The available photo sizes.
    /// </summary>
    public IReadOnlyList<PhotoSize> Sizes { get; }

    /// <summary>
    /// The largest available photo size.
    /// </summary>
    public PhotoSize Large => Sizes[^1];

    /// <summary>
    /// The smallest available photo size.
    /// </summary>
    public PhotoSize Small => Sizes[0];
}
