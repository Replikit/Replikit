using System.Diagnostics;
using Replikit.Abstractions.Common.Exceptions;

namespace Replikit.Abstractions.Common.Models;

[DebuggerDisplay("[Value = {Value}]")]
public record Identifier
{
    public object Value { get; }

    public Identifier(string value) => Value = value;
    public Identifier(int value) => Value = value;
    public Identifier(long value) => Value = value;

    public Identifier(object value)
    {
        if (value is not string && value is not long && value is not int)
            throw new InvalidIdentifierValueException(value);

        Value = value;
    }

    public static implicit operator Identifier(string value) => new(value);
    public static implicit operator Identifier(int value) => new(value);
    public static implicit operator Identifier(long value) => new(value);

    public static implicit operator string(Identifier identifier)
    {
        if (identifier.Value is string value)
        {
            return value;
        }

        throw new InvalidIdentifierException(identifier, typeof(string));
    }

    public static implicit operator int(Identifier identifier)
    {
        if (identifier.Value is int value)
        {
            return value;
        }

        throw new InvalidIdentifierException(identifier, typeof(int));
    }

    public static implicit operator long(Identifier identifier)
    {
        if (identifier.Value is long value)
        {
            return value;
        }

        throw new InvalidIdentifierException(identifier, typeof(long));
    }

    public override string? ToString() => Value.ToString();
}
