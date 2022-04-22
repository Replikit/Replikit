using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Common.Exceptions;

public class InvalidIdentifierException : ReplikitDomainException
{
    public Identifier Identifier { get; }

    public InvalidIdentifierException(Identifier identifier, Type expectedType) : base(
        $"Invalid identifier {identifier.Value}. " +
        $"Expected type {expectedType.Name}, got {identifier.Value.GetType().Name}")
    {
        Identifier = identifier;
    }
}
