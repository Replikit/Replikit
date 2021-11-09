namespace Replikit.Abstractions.Messages.Exceptions;

public class EmptyMessageIdentifierException : ReplikitMessageException
{
    public EmptyMessageIdentifierException() : base("Empty message identifier") { }
}
