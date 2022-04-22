using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Attachments.Models;

public sealed record PhotoSize(
    GlobalIdentifier Id,
    string? Caption = null,
    string? Url = null,
    string? FileName = null,
    Stream? Content = null,
    string? UploadId = null,
    int? Width = null,
    int? Height = null,
    int? Size = null
) : Attachment(Id, Caption, Url, FileName, Content, UploadId);
