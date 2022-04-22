namespace Replikit.Abstractions.Common.Exceptions;

public class ForbiddenIdentifierValue : ReplikitDomainException
{
    public ForbiddenIdentifierValue(object value) : base(
        $"Invalid identifier value {value} of type {value.GetType().Name}. " +
        "Only string, long (int) or guid are allowed.") { }
}
