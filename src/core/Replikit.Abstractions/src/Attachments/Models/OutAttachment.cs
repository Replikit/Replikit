using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;

namespace Replikit.Abstractions.Attachments.Models;

public record OutAttachment
{
    public AttachmentType Type { get; }
    public object Source { get; }
    public string? Caption { get; }
    public string? FileName { get; }

    private OutAttachment(AttachmentType type, object source, string? caption)
    {
        Type = type;
        Source = source;
        Caption = caption;
    }

    public OutAttachment(AttachmentType type, Stream stream, string? fileName = null, string? caption = null) :
        this(type, (object) stream, caption)
    {
        FileName = fileName;
    }

    public OutAttachment(AttachmentType type, string url, string? caption = null) :
        this(type, (object) url, caption) { }

    public OutAttachment(AttachmentType type, Identifier uploadId, string? caption = null) :
        this(type, (object) uploadId, caption) { }

    public OutAttachment(AttachmentType type, FileInfo fileInfo, string? caption = null) :
        this(type, (object) fileInfo, caption)
    {
        FileName = fileInfo.Name;
    }

    public static OutAttachment FromContent(AttachmentType type, Stream stream, string? fileName = null,
        string? caption = null)
    {
        return new OutAttachment(type, stream, fileName, caption);
    }

    public static OutAttachment FromUrl(AttachmentType type, string url, string? caption = null)
    {
        return new OutAttachment(type, url, caption);
    }

    public static OutAttachment FromUploadId(AttachmentType type, Identifier uploadId, string? caption = null)
    {
        return new OutAttachment(type, uploadId, caption);
    }

    public static OutAttachment FromFile(AttachmentType type, string filePath, string? caption = null)
    {
        return new OutAttachment(type, new FileInfo(filePath), caption);
    }
}
