using Replikit.Abstractions.Common.Exceptions;
using Replikit.Abstractions.Common.Utilities;

namespace Replikit.Abstractions.Common.Models;

/// <summary>
/// The wrapper around primitive identifier.
/// Can contain long, int, string or Guid as identifier value.
/// </summary>
public readonly record struct Identifier
{
    private readonly object _value;

    /// <summary>
    /// Creates new identifier from int value.
    /// </summary>
    /// <param name="value">An identifier value.</param>
    public Identifier(int value)
    {
        _value = value;
    }

    /// <summary>
    /// Creates new identifier from long value.
    /// </summary>
    /// <param name="value">An identifier value.</param>
    public Identifier(long value)
    {
        _value = value;
    }

    /// <summary>
    /// Creates new identifier from string value.
    /// </summary>
    /// <param name="value">An identifier value.</param>
    public Identifier(string value)
    {
        _value = Check.NotNull(value);
    }

    /// <summary>
    /// Creates new identifier from Guid value.
    /// </summary>
    /// <param name="value">An identifier value.</param>
    public Identifier(Guid value)
    {
        _value = value;
    }

    /// <summary>
    /// Gets the underlying boxed value.
    /// </summary>
    /// <returns>The underlying value or null if the identifier is default.</returns>
    public object? GetUnderlyingValue() => _value;

    /// <summary>
    /// Creates new identifier from int value.
    /// </summary>
    /// <param name="value">An identifier value.</param>
    /// <returns>The <see cref="Identifier"/>.</returns>
    public static implicit operator Identifier(int value) => new(value);

    /// <summary>
    /// Creates new identifier from long value.
    /// </summary>
    /// <param name="value">An identifier value.</param>
    /// <returns>The <see cref="Identifier"/>.</returns>
    public static implicit operator Identifier(long value) => new(value);

    /// <summary>
    /// Creates new identifier from string value.
    /// </summary>
    /// <param name="value">An identifier value.</param>
    /// <returns>The <see cref="Identifier"/>.</returns>
    public static implicit operator Identifier(string value) => new(value);

    /// <summary>
    /// Creates new identifier from Guid value.
    /// </summary>
    /// <param name="value">An identifier value.</param>
    /// <returns>The <see cref="Identifier"/>.</returns>
    public static implicit operator Identifier(Guid value) => new(value);

    /// <summary>
    /// Unwraps the identifier value as int.
    /// </summary>
    /// <param name="identifier">The identifier which value should be unwrapped.</param>
    /// <returns>The int value.</returns>
    /// <exception cref="InvalidIdentifierValueException">The identifier value is not int.</exception>
    public static implicit operator int(Identifier identifier)
    {
        if (identifier._value is null)
        {
            throw new EmptyIdentifierValueException(typeof(string));
        }

        return identifier._value switch
        {
            int intValue => intValue,
            long longValue => (int) longValue,
            _ => throw new InvalidIdentifierValueException(identifier, typeof(int))
        };
    }

    /// <summary>
    /// Unwraps the identifier value as long.
    /// </summary>
    /// <param name="identifier">The identifier which value should be unwrapped.</param>
    /// <returns>The long value.</returns>
    /// <exception cref="InvalidIdentifierValueException">The identifier value is not long.</exception>
    public static implicit operator long(Identifier identifier)
    {
        if (identifier._value is null)
        {
            throw new EmptyIdentifierValueException(typeof(string));
        }

        return identifier._value switch
        {
            int intValue => intValue,
            long longValue => longValue,
            _ => throw new InvalidIdentifierValueException(identifier, typeof(long))
        };
    }

    /// <summary>
    /// Unwraps the identifier value as string.
    /// </summary>
    /// <param name="identifier">The identifier which value should be unwrapped.</param>
    /// <returns>The string value.</returns>
    /// <exception cref="InvalidIdentifierValueException">The identifier value is not a string.</exception>
    public static implicit operator string(Identifier identifier)
    {
        if (identifier._value is null)
        {
            throw new EmptyIdentifierValueException(typeof(string));
        }

        if (identifier._value is not string value)
        {
            throw new InvalidIdentifierValueException(identifier._value, typeof(string));
        }

        return value;
    }

    /// <summary>
    /// Unwraps the identifier value as Guid.
    /// </summary>
    /// <param name="identifier">The identifier which value should be unwrapped.</param>
    /// <returns>The Guid value.</returns>
    /// <exception cref="InvalidIdentifierValueException">The identifier value is not a Guid.</exception>
    public static implicit operator Guid(Identifier identifier)
    {
        if (identifier._value is null)
        {
            throw new EmptyIdentifierValueException(typeof(string));
        }

        if (identifier._value is not Guid value)
        {
            throw new InvalidIdentifierValueException(identifier, typeof(Guid));
        }

        return value;
    }

    public bool Equals(Identifier other)
    {
        if (ExtractNumber() is { } thisNumber && other.ExtractNumber() is { } otherNumber)
        {
            return thisNumber == otherNumber;
        }

        return _value == other._value;
    }

    public override int GetHashCode()
    {
        if (ExtractNumber() is { } number)
        {
            return number.GetHashCode();
        }

        return _value?.GetHashCode() ?? 0;
    }

    /// <summary>
    /// Returns a string representation of the identifier value.
    /// </summary>
    /// <returns>A string representation of the identifier value or null if the identifier is default.</returns>
    public override string? ToString()
    {
        return GetUnderlyingValue()?.ToString();
    }

    /// <summary>
    /// Tries to parse the identifier from string.
    /// </summary>
    /// <param name="value">The string value.</param>
    /// <param name="identifier">The parsed identifier.</param>
    /// <returns>True if the identifier was parsed successfully.</returns>
    public static bool TryParse(string? value, out Identifier identifier)
    {
        if (value is null)
        {
            identifier = default;
            return false;
        }

        if (int.TryParse(value, out var intValue))
        {
            identifier = intValue;
            return true;
        }

        if (long.TryParse(value, out var longValue))
        {
            identifier = longValue;
            return true;
        }

        if (Guid.TryParse(value, out var guidValue))
        {
            identifier = guidValue;
            return true;
        }

        identifier = value;
        return true;
    }

    private long? ExtractNumber()
    {
        return _value switch
        {
            int intValue => intValue,
            long longValue => longValue,
            _ => null
        };
    }
}
