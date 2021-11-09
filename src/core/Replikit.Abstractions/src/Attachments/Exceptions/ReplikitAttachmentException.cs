using Replikit.Abstractions.Common.Exceptions;

namespace Replikit.Abstractions.Attachments.Exceptions;

public class ReplikitAttachmentException : ReplikitDomainException
{
    public ReplikitAttachmentException(string? message) : base(message) { }
}
