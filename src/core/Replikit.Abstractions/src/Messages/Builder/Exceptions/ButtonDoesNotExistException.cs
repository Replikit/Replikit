namespace Replikit.Abstractions.Messages.Builder.Exceptions;

public class ButtonDoesNotExistException : MessageBuilderException
{
    public ButtonDoesNotExistException(int row, int column) : base($"Button ({row}; {column}) does not exist") { }
}
