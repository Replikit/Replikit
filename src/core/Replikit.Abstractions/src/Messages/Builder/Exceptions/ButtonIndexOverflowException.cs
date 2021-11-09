namespace Replikit.Abstractions.Messages.Builder.Exceptions;

public class ButtonIndexOverflowException : MessageBuilderException
{
    public ButtonIndexOverflowException(int index, string parameterName) : base(
        $"Index of button {parameterName} is overflowed by more than one. Got {index}") { }
}
