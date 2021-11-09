namespace Replikit.Extensions.Sessions.Exceptions;

public class UnsupportedSessionTypeException : SessionsException
{
    public UnsupportedSessionTypeException(string? message) : base(message) { }
}
