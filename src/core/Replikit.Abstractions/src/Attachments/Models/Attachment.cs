using System.Collections.Immutable;
using Replikit.Abstractions.Common.CustomData;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Common.Utilities;

namespace Replikit.Abstractions.Attachments.Models;

/// <summary>
/// The attachment that is attached to a message.
/// </summary>
public abstract class Attachment : IHasCustomData
{
    /// <summary>
    /// Creates a new instance of <see cref="Attachment"/>.
    /// </summary>
    /// <param name="id">An identifier of the attachment.</param>
    /// <param name="type">A type of the attachment.</param>
    protected Attachment(GlobalIdentifier id, AttachmentType type)
    {
        Id = Check.NotDefault(id);
        Type = type;
    }

    /// <summary>
    /// The global identifier of the attachment.
    /// </summary>
    public GlobalIdentifier Id { get; }

    /// <summary>
    /// The type of the attachment.
    /// </summary>
    public AttachmentType Type { get; }

    /// <summary>
    /// The caption that should be displayed with the attachment.
    /// </summary>
    public string? Caption { get; init; }

    /// <summary>
    /// The URL of the attachment.
    /// <br/>
    /// May be null if the platform does not expose an attachment URL or it is not accessible to the bot.
    /// </summary>
    public Uri? Url { get; init; }

    /// <summary>
    /// The file name of the attachment.
    /// <br/>
    /// May be null if the platform does not expose an attachment file name or it is not accessible to the bot.
    /// </summary>
    public string? FileName { get; init; }

    /// <summary>
    /// The size of the attachment in bytes.
    /// <br/>
    /// May be null if the platform does not expose an attachment size or it is not accessible to the bot.
    /// </summary>
    public int? FileSize { get; init; }

    /// <summary>
    /// The special identifier that on some platforms should be used to send the attachment from the bot.
    /// <br/>
    /// May be null if the platform does not distinguish between attachment id and upload id.
    /// </summary>
    public Identifier? UploadId { get; init; }

    public IReadOnlyList<object> CustomData { get; init; } = ImmutableArray<object>.Empty;
}
