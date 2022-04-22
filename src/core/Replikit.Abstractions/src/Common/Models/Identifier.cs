using Replikit.Abstractions.Common.Exceptions;

namespace Replikit.Abstractions.Common.Models;

public readonly record struct Identifier
{
    public object Value { get; }

    public Identifier(long value)
    {
        Value = value;
    }

    public Identifier(int value)
    {
        Value = value;
    }

    public Identifier(string value)
    {
        Value = value;
    }

    public Identifier(Guid value)
    {
        Value = value;
    }

    public Identifier(object value)
    {
        if (value is not (long or int or string or Guid))
        {
            throw new ForbiddenIdentifierValue(value);
        }

        Value = value;
    }

    public static implicit operator Identifier(int value) => new(value);
    public static implicit operator Identifier(long value) => new(value);
    public static implicit operator Identifier(string value) => new(value);
    public static implicit operator Identifier(Guid value) => new(value);

    public static implicit operator string(Identifier identifier)
    {
        if (identifier.Value is not string value)
        {
            throw new InvalidIdentifierException(identifier, typeof(string));
        }

        return value;
    }

    public static implicit operator Guid(Identifier identifier)
    {
        if (identifier.Value is not Guid value)
        {
            throw new InvalidIdentifierException(identifier, typeof(Guid));
        }

        return value;
    }

    public static implicit operator int(Identifier identifier)
    {
        return identifier.Value switch
        {
            int intValue => intValue,
            long longValue => (int) longValue,
            _ => throw new InvalidIdentifierException(identifier, typeof(int))
        };
    }

    public static implicit operator long(Identifier identifier)
    {
        return identifier.Value switch
        {
            int intValue => intValue,
            long longValue => longValue,
            _ => throw new InvalidIdentifierException(identifier, typeof(long))
        };
    }

    public bool Equals(Identifier other)
    {
        if (ExtractNumber() is { } thisNumber && other.ExtractNumber() is { } otherNumber)
        {
            return thisNumber == otherNumber;
        }

        return Value == other.Value;
    }

    public override int GetHashCode()
    {
        if (ExtractNumber() is { } number)
        {
            return number.GetHashCode();
        }

        return Value?.GetHashCode() ?? 0;
    }

    private long? ExtractNumber()
    {
        return Value switch
        {
            int intValue => intValue,
            long longValue => longValue,
            _ => null
        };
    }

    public override string ToString()
    {
        return Value?.ToString() ?? "null";
    }
}
