using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Attachments.Models;

/// <summary>
/// The audio attachment.
/// </summary>
public class AudioAttachment : Attachment
{
    /// <summary>
    /// Creates a new instance of <see cref="AudioAttachment"/>.
    /// </summary>
    /// <param name="id">An identifier of the attachment.</param>
    public AudioAttachment(GlobalIdentifier id) : base(id, AttachmentType.Audio) { }

    /// <summary>
    /// The duration of the audio file in seconds.
    /// </summary>
    public int? Duration { get; init; }
}
