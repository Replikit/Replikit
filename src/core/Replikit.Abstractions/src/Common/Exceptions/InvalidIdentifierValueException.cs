using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Resources;

namespace Replikit.Abstractions.Common.Exceptions;

/// <summary>
/// Represents an error occurred while using the <see cref="Identifier"/> which wraps the value of wrong type.
/// </summary>
public class InvalidIdentifierValueException : ReplikitException
{
    /// <summary>
    /// Creates a new instance of <see cref="InvalidIdentifierValueException"/>.
    /// </summary>
    /// <param name="identifier">An identifier which value has invalid type.</param>
    /// <param name="expectedType">An expected type of the identifier value.</param>
    public InvalidIdentifierValueException(Identifier identifier, Type expectedType) :
        base(string.Format(Strings.InvalidIdentifierValue, identifier.ToString(), expectedType.Name)) { }
}
