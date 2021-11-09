using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;

namespace Replikit.Abstractions.Attachments.Models;

public abstract record Attachment(GlobalIdentifier? Id = null, string? Caption = null,
    string? Url = null, string? FileName = null, Stream? Content = null)
{
    public static implicit operator OutMessage(Attachment attachment) => OutMessage.FromAttachment(attachment);
}
