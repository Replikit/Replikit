namespace Replikit.Core.Common;

public class DynamicValue : IDynamicValue
{
    private readonly Func<Type, object?> _deserializeDelegate;

    public DynamicValue(Func<Type, object?> deserializeDelegate)
    {
        _deserializeDelegate = deserializeDelegate;
    }

    public object? Deserialize(Type valueType)
    {
        return _deserializeDelegate(valueType);
    }
}
