namespace Replikit.Abstractions.Attachments.Models;

/// <summary>
/// The type of the attachment.
/// </summary>
public enum AttachmentType : byte
{
    /// <summary>
    /// The type of the attachment is unknown.
    /// </summary>
    Unknown,

    /// <summary>
    /// The attachment is an audio file.
    /// </summary>
    Audio,

    /// <summary>
    /// The attachment is a document.
    /// </summary>
    Document,

    /// <summary>
    /// The attachment is an image.
    /// </summary>
    Photo,

    /// <summary>
    /// The attachment is a sticker.
    /// </summary>
    Sticker,

    /// <summary>
    /// The attachment is a video.
    /// </summary>
    Video,

    /// <summary>
    /// The attachment is a voice message.
    /// </summary>
    Voice
}
