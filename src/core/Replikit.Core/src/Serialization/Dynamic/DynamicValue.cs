using Replikit.Abstractions.Common.Exceptions;
using Replikit.Core.Resources;

namespace Replikit.Core.Serialization.Dynamic;

/// <summary>
/// The wrapper for value that requires a type to be deserialized.
/// </summary>
public readonly struct DynamicValue
{
    private readonly object? _value;
    private readonly Func<object?, Type, object?>? _factory;

    private DynamicValue(object? value, Func<object?, Type, object?>? factory)
    {
        _value = value;
        _factory = factory;
    }

    /// <summary>
    /// Creates a new instance of <see cref="DynamicValue"/> from a value.
    /// </summary>
    /// <param name="value">The value to wrap.</param>
    /// <returns>A new instance of <see cref="DynamicValue"/>.</returns>
    public static DynamicValue FromValue(object? value) => new(value, null);

    /// <summary>
    /// Creates a new instance of <see cref="DynamicValue"/> from a serialized value and a factory
    /// that can deserialize it when the type is known.
    /// </summary>
    /// <param name="value">The serialized value.</param>
    /// <param name="factory">The deserialization factory.</param>
    /// <returns>A new instance of <see cref="DynamicValue"/>.</returns>
    public static DynamicValue FromFactory(object? value, Func<object?, Type, object?> factory)
    {
        return new DynamicValue(value, factory);
    }

    /// <summary>
    /// Gets the underlying value.
    /// <br/>
    /// It can be deserialized value as well as serialized value.
    /// Should be used only by serializers.
    /// For deserialization use <see cref="Deserialize(Type)"/> or <see cref="Deserialize{TValue}"/>.
    /// </summary>
    /// <returns></returns>
    public object? GetUnderlyingValue() => _value;

    /// <summary>
    /// Deserializes the value to the specified type.
    /// </summary>
    /// <param name="type">The type to deserialize.</param>
    /// <returns>The deserialized value.</returns>
    public object? Deserialize(Type type)
    {
        if (_factory is not null)
        {
            return _factory.Invoke(_value, type);
        }

        if (_value is null)
        {
            return null;
        }

        if (type.IsInstanceOfType(_value))
        {
            return _value;
        }

        throw new ReplikitException(string.Format(Strings.InvalidWrappedValueType, _value.GetType()));
    }

    /// <summary>
    /// Deserializes the value to the specified type.
    /// </summary>
    /// <typeparam name="TValue">The type to deserialize.</typeparam>
    /// <returns>The deserialized value.</returns>
    public TValue? Deserialize<TValue>()
    {
        return (TValue?)Deserialize(typeof(TValue));
    }
}