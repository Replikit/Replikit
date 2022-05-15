namespace Replikit.Core.Common;

public class DynamicValue
{
    private readonly Func<Type, object?>? _valueResolver;

    public DynamicValue(object? value)
    {
        Value = value;
        _valueResolver = null;
    }

    public DynamicValue(Func<Type, object?>? valueResolver)
    {
        Value = null;
        _valueResolver = valueResolver;
    }

    public object? Value { get; }

    public object? GetValue(Type type)
    {
        return Value ?? _valueResolver?.Invoke(type);
    }
}
