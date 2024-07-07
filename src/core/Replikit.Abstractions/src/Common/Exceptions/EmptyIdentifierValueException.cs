using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Resources;

namespace Replikit.Abstractions.Common.Exceptions;

/// <summary>
/// Represents an error occurred while using the default <see cref="Identifier"/> value which doesn't contain any value.
/// </summary>
public sealed class EmptyIdentifierValueException : ReplikitException
{
    internal EmptyIdentifierValueException(Type expectedType) :
        base(string.Format(Strings.EmptyIdentifierValue, expectedType.Name)) { }
}
