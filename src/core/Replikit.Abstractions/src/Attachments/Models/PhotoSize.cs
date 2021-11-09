using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Attachments.Models;

public sealed record PhotoSize(
    GlobalIdentifier? Id = null,
    string? Caption = null,
    string? Url = null,
    string? FileName = null,
    Stream? Content = null,
    int? Width = null,
    int? Height = null,
    int? Size = null
) : AttachmentBase<PhotoSize>(Id, Caption, Url, FileName, Content)
{
    public PhotoSize() : this(Id: null) { }
}
