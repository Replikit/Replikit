namespace Replikit.Core.Common;

public static class DynamicValueHelper
{
    public static object? Deserialize(object? value, Type valueType)
    {
        ArgumentNullException.ThrowIfNull(valueType);

        return value is IDynamicValue dynamicValue ? dynamicValue.Deserialize(valueType) : value;
    }

    public static TValue? Deserialize<TValue>(object? value)
    {
        return (TValue?) Deserialize(value, typeof(TValue));
    }
}
