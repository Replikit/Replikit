using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Attachments.Models;

public record AudioAttachment(
    GlobalIdentifier Id,
    string? Caption = null,
    string? Url = null,
    string? FileName = null,
    Stream? Content = null,
    string? UploadId = null,
    string? Title = null,
    int? Size = null,
    int? Duration = null
) : Attachment(Id, Url, Caption, FileName, Content, UploadId);
