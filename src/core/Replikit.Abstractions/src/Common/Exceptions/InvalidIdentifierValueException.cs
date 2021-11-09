namespace Replikit.Abstractions.Common.Exceptions;

public class InvalidIdentifierValueException : ReplikitDomainException
{
    public InvalidIdentifierValueException(object value) : base(
        $"Invalid identifier value {value} of type {value.GetType().Name}. Only string, int or long are allowed.") { }
}
