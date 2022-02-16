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
        return identifier.Value switch
        {
            string value => value,
            _ => throw new InvalidIdentifierException(identifier, typeof(string))
        };
    }

    public static implicit operator int(Identifier identifier)
    {
        return identifier.Value switch
        {
            int value => value,
            _ => throw new InvalidIdentifierException(identifier, typeof(int))
        };
    }

    public static implicit operator long(Identifier identifier)
    {
        return identifier.Value switch
        {
            long longValue => longValue,
            int intValue => intValue,
            _ => throw new InvalidIdentifierException(identifier, typeof(long))
        };
    }

    public override string? ToString() => Value.ToString();
}
