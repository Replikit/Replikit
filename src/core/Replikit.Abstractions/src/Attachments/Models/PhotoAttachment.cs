using Replikit.Abstractions.Attachments.Exceptions;

namespace Replikit.Abstractions.Attachments.Models;

public record PhotoAttachment(IReadOnlyList<PhotoSize> Sizes) :
    Attachment(
        Sizes.Count > 0 ? Sizes[0].Id : throw new InvalidPhotoAttachmentException(),
        Sizes[0].Caption,
        Sizes[0].Url,
        Sizes[0].FileName,
        Sizes[0].Content,
        Sizes[0].UploadId
    )
{
    public PhotoSize Large => Sizes[^1];
    public PhotoSize Small => Sizes[0];
}
