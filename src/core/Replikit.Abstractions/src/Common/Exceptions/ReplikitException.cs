namespace Replikit.Abstractions.Common.Exceptions;

public class ReplikitException : Exception
{
    public ReplikitException(string? message) : base(message) { }
    public ReplikitException(string? message, Exception? innerException) : base(message, innerException) { }
}
