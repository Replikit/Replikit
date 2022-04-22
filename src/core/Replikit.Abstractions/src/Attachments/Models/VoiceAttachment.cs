using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Attachments.Models;

public record VoiceAttachment(
    GlobalIdentifier Id,
    string? Caption = null,
    string? Url = null,
    string? FileName = null,
    Stream? Content = null,
    string? UploadId = null,
    int? Size = null,
    int? Duration = null
) : Attachment(Id, Caption, Url, FileName, Content, UploadId);
