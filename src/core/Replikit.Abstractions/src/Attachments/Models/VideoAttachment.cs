using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Attachments.Models;

/// <summary>
/// The video attachment.
/// </summary>
public class VideoAttachment : Attachment
{
    /// <summary>
    /// Creates a new instance of <see cref="VideoAttachment"/>.
    /// </summary>
    /// <param name="id">An identifier of the attachment.</param>
    public VideoAttachment(GlobalIdentifier id) : base(id, AttachmentType.Video) { }

    /// <summary>
    /// The duration of the video in seconds.
    /// </summary>
    public int? Duration { get; init; }
}
