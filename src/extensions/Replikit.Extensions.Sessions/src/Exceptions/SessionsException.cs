using Replikit.Abstractions.Common.Exceptions;

namespace Replikit.Extensions.Sessions.Exceptions;

public class SessionsException : ReplikitException
{
    public SessionsException(string? message) : base(message) { }
}
