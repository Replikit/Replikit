using Replikit.Abstractions.Messages.Exceptions;

namespace Replikit.Abstractions.Messages.Builder.Exceptions;

public class MessageBuilderException : ReplikitMessageException
{
    public MessageBuilderException(string message) : base(message) { }
}
