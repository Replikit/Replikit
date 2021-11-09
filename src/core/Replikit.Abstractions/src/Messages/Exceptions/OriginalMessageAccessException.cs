namespace Replikit.Abstractions.Messages.Exceptions;

public class OriginalMessageAccessException : ReplikitMessageException
{
    public OriginalMessageAccessException(Exception? innerException)
        : base("Failed to get original message", innerException) { }
}
