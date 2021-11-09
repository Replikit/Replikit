using Replikit.Abstractions.Attachments.Exceptions;
using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Attachments.Models;

public record PhotoAttachment(IReadOnlyList<PhotoSize> Sizes) :
    Attachment(
        Sizes.Count > 0 ? Sizes[0].Id : throw new InvalidPhotoAttachmentException(),
        Sizes[0].Caption,
        Sizes[0].Url,
        Sizes[0].FileName,
        Sizes[0].Content
    )
{
    public PhotoSize Large => Sizes[^1];
    public PhotoSize Small => Sizes[0];

    public static PhotoAttachment FromSize(PhotoSize size) => new(new[] { size });

    public static PhotoAttachment FromUrl(string url,
        string? fileName = null,
        string? caption = null,
        GlobalIdentifier? id = null) => FromSize(PhotoSize.FromUrl(url, fileName, caption));

    public static PhotoAttachment FromContent(Stream content,
        string? fileName = null,
        string? caption = null,
        GlobalIdentifier? id = null) => FromSize(PhotoSize.FromContent(content, fileName, caption, id));

    public static PhotoAttachment FromFile(string path,
        string? fileName = null,
        string? caption = null,
        GlobalIdentifier? id = null) => FromSize(PhotoSize.FromFile(path, fileName, caption, id));
}
