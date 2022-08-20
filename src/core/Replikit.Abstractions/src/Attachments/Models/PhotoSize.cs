using System.Collections.Immutable;
using Replikit.Abstractions.Common.CustomData;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Common.Utilities;

namespace Replikit.Abstractions.Attachments.Models;

/// <summary>
/// Represents a one of the available photo sizes.
/// </summary>
public class PhotoSize : IHasCustomData
{
    /// <summary>
    /// Creates a new instance of <see cref="PhotoSize"/>.
    /// </summary>
    /// <param name="id">An identifier of the photo size.</param>
    public PhotoSize(GlobalIdentifier id)
    {
        Id = Check.NotDefault(id);
    }

    /// <summary>
    /// The identifier of the photo size.
    /// </summary>
    public GlobalIdentifier Id { get; }

    /// <summary>
    /// The URL of the photo size.
    /// <br/>
    /// May be null if the platform does not expose a photo size URL or it is not accessible to the bot.
    /// </summary>
    public Uri? Url { get; init; }

    /// <summary>
    /// The size of the attachment in bytes.
    /// <br/>
    /// May be null if the platform does not expose an attachment size or it is not accessible to the bot.
    /// </summary>
    public int? FileSize { get; init; }

    /// <summary>
    /// The width of the photo size in pixels.
    /// <br/>
    /// May be null if the platform does not expose a photo size width or it is not accessible to the bot.
    /// </summary>
    public int? Width { get; init; }

    /// <summary>
    /// The height of the photo size in pixels.
    /// <br/>
    /// May be null if the platform does not expose a photo size height or it is not accessible to the bot.
    /// </summary>
    public int? Height { get; init; }

    /// <summary>
    /// The special identifier that on some platforms should be used to send the attachment from the bot.
    /// <br/>
    /// May be null if the platform does not distinguish between attachment id and upload id.
    /// </summary>
    public Identifier? UploadId { get; init; }

    public IReadOnlyList<object> CustomData { get; init; } = ImmutableArray<object>.Empty;
}
