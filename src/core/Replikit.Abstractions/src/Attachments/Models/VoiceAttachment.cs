using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Attachments.Models;

public record VoiceAttachment(
    GlobalIdentifier? Id,
    string? Caption = null,
    string? Url = null,
    string? FileName = null,
    Stream? Content = null,
    int? Size = null,
    int? Duration = null
) : AttachmentBase<VoiceAttachment>(Id, Caption, Url, FileName, Content)
{
    public VoiceAttachment() : this(Id: null) { }
}
