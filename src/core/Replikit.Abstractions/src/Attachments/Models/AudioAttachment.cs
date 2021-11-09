using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Attachments.Models;

public record AudioAttachment(
    GlobalIdentifier? Id = null,
    string? Caption = null,
    string? Url = null,
    string? FileName = null,
    Stream? Content = null,
    string? Title = null,
    int? Size = null,
    int? Duration = null
) : AttachmentBase<AudioAttachment>(Id, Url, Caption, FileName, Content)
{
    public AudioAttachment() : this(Id: null) { }
}
