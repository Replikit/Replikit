namespace Replikit.Abstractions.Common.Exceptions;

public class ReplikitDomainException : ReplikitException
{
    public ReplikitDomainException(string? message) : base(message) { }
    public ReplikitDomainException(string? message, Exception? innerException) : base(message, innerException) { }
}
