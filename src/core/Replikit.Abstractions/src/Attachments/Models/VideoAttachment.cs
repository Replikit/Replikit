using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Attachments.Models;

public record VideoAttachment(
    GlobalIdentifier? Id = null,
    string? Caption = null,
    string? Url = null,
    string? FileName = null,
    Stream? Content = null,
    int? Size = null,
    int? Duration = null
) : AttachmentBase<VideoAttachment>(Id, Caption, Url, FileName, Content)
{
    public VideoAttachment() : this(Id: null) { }
}
