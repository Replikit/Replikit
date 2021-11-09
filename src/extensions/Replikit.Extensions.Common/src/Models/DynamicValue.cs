namespace Replikit.Extensions.Common.Models;

public class DynamicValue
{
    private readonly Func<Type, object?>? _valueResolver;

    public DynamicValue(object? value)
    {
        Value = value;
    }

    public DynamicValue(Func<Type, object?>? valueResolver)
    {
        _valueResolver = valueResolver;
    }

    public object? Value { get; }

    public object? GetValue(Type type)
    {
        return Value ?? _valueResolver?.Invoke(type);
    }
}
