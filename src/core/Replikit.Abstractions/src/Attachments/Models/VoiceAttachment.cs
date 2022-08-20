using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Attachments.Models;

/// <summary>
/// The voice message attachment.
/// </summary>
public class VoiceAttachment : Attachment
{
    /// <summary>
    /// Creates a new instance of <see cref="VoiceAttachment"/>.
    /// </summary>
    /// <param name="id">An identifier of the attachment.</param>
    public VoiceAttachment(GlobalIdentifier id) : base(id, AttachmentType.Voice) { }

    /// <summary>
    /// The duration of the voice message in seconds.
    /// </summary>
    public int? Duration { get; init; }
}
