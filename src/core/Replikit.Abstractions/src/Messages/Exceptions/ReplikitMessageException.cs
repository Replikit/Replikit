using Replikit.Abstractions.Common.Exceptions;

namespace Replikit.Abstractions.Messages.Exceptions;

public class ReplikitMessageException : ReplikitDomainException
{
    public ReplikitMessageException(string? message) : base(message) { }

    public ReplikitMessageException(string? message, Exception? innerException) : base(message, innerException) { }
}
