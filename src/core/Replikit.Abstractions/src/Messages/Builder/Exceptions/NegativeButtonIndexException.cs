namespace Replikit.Abstractions.Messages.Builder.Exceptions;

public class NegativeButtonIndexException : MessageBuilderException
{
    public NegativeButtonIndexException(int index, string parameterName) : base(
        $"Index of button {parameterName} must be non-negative. Got {index}") { }
}
