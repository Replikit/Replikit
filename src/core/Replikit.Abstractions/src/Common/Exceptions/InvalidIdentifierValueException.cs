using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Resources;

namespace Replikit.Abstractions.Common.Exceptions;

/// <summary>
/// Represents an error occurred while using the <see cref="Identifier"/> which wraps the value of wrong type.
/// </summary>
public sealed class InvalidIdentifierValueException : ReplikitException
{
    internal InvalidIdentifierValueException(object value, Type expectedType) :
        base(string.Format(Strings.InvalidIdentifierValue, expectedType.Name, value.GetType().Name)) { }
}
