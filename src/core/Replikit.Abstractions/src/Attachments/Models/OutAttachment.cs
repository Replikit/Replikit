using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Common.Utilities;
using Replikit.Abstractions.Resources;

namespace Replikit.Abstractions.Attachments.Models;

/// <summary>
/// Represents a model of the outgoing attachment that will be sent by the bot.
/// </summary>
public class OutAttachment
{
    private OutAttachment(AttachmentType type, object source, string? caption, string? fileName, string? cacheKey)
    {
        Type = Check.NotUnknown(type);
        Source = source;
        Caption = caption;
        FileName = fileName;
        CacheKey = cacheKey;
    }

    /// <summary>
    /// The type of the attachment.
    /// </summary>
    public AttachmentType Type { get; }

    /// <summary>
    /// The source of the attachment that should be used by the adapter to send the attachment.
    /// <br/>
    /// Can be of four types: <see cref="Identifier"/>, <see cref="Stream"/>, <see cref="FileInfo"/>, <see cref="Uri"/>.
    /// </summary>
    public object Source { get; }

    /// <summary>
    /// The caption of the attachment.
    /// </summary>
    public string? Caption { get; }

    /// <summary>
    /// The file name of the attachment.
    /// </summary>
    public string? FileName { get; }

    /// <summary>
    /// The string-based key used to resolve the attachment from the cache or store it.
    /// </summary>
    public string? CacheKey { get; }

    /// <summary>
    /// Creates a new <see cref="OutAttachment"/> from the specified content stream.
    /// </summary>
    /// <param name="type">A type of the attachment.</param>
    /// <param name="content">A content stream. Must be readable.</param>
    /// <param name="caption">A caption of the attachment.</param>
    /// <param name="fileName">A file name of the attachment.</param>
    /// <param name="cacheKey">A key to resolve or cache the attachment.</param>
    /// <returns>A created <see cref="OutAttachment"/>.</returns>
    /// <exception cref="ArgumentException">The specified <paramref name="content"/> stream is not readable.</exception>
    public static OutAttachment FromContent(AttachmentType type, Stream content, string? caption = null,
        string? fileName = null, string? cacheKey = null)
    {
        Check.NotNull(content);

        if (!content.CanRead)
        {
            throw new ArgumentException(Strings.StreamMustBeReadable, nameof(content));
        }

        return new OutAttachment(type, content, caption, fileName, cacheKey);
    }

    /// <summary>
    /// Creates a new <see cref="OutAttachment"/> from the specified url.
    /// </summary>
    /// <param name="type">A type of the attachment.</param>
    /// <param name="url">A url of the attachment.</param>
    /// <param name="caption">A caption of the attachment.</param>
    /// <param name="fileName">A file name of the attachment.</param>
    /// <param name="cacheKey">A key to resolve or cache the attachment.</param>
    /// <returns>A created <see cref="OutAttachment"/>.</returns>
    public static OutAttachment FromUrl(AttachmentType type, Uri url, string? caption = null, string? fileName = null,
        string? cacheKey = null)
    {
        Check.NotNull(url);

        return new OutAttachment(type, url, caption, fileName, cacheKey);
    }

    /// <summary>
    /// Creates a new <see cref="OutAttachment"/> from the specified uploadId.
    /// </summary>
    /// <param name="type">A type of the attachment.</param>
    /// <param name="uploadId">An uploadId of the attachment.</param>
    /// <param name="caption">A caption of the attachment.</param>
    /// <param name="fileName">A file name of the attachment.</param>
    /// <returns>A created <see cref="OutAttachment"/>.</returns>
    public static OutAttachment FromUploadId(AttachmentType type, Identifier uploadId, string? caption = null,
        string? fileName = null)
    {
        Check.NotDefault(uploadId);

        return new OutAttachment(type, uploadId, caption, fileName, null);
    }

    /// <summary>
    /// Creates a new <see cref="OutAttachment"/> from the file with the specified path.
    /// </summary>
    /// <param name="type">A type of the attachment.</param>
    /// <param name="filePath">A path to the file.</param>
    /// <param name="caption">A caption of the attachment.</param>
    /// <param name="fileName">A file name of the attachment.</param>
    /// <param name="cacheKey">A key to resolve or cache the attachment.</param>
    /// <returns>A created <see cref="OutAttachment"/>.</returns>
    public static OutAttachment FromFile(AttachmentType type, string filePath, string? caption = null,
        string? fileName = null, string? cacheKey = null)
    {
        Check.NotNullOrWhiteSpace(filePath);

        return new OutAttachment(type, new FileInfo(filePath), caption, fileName, cacheKey);
    }
}
