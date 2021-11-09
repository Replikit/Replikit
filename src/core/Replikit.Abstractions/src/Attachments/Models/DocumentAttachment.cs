using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Attachments.Models;

public record DocumentAttachment(
    GlobalIdentifier? Id,
    string? Caption = null,
    string? Url = null,
    string? FileName = null,
    Stream? Content = null,
    int? Size = null
) : Attachment(Id, Caption, Url, FileName, Content)
{
    public DocumentAttachment() : this(Id: null) { }
}
