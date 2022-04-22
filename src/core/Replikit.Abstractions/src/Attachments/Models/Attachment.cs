using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Attachments.Models;

public abstract record Attachment(
    GlobalIdentifier Id,
    string? Caption,
    string? Url,
    string? FileName,
    Stream? Content,
    string? UploadId
);
