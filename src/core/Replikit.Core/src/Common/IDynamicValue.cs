namespace Replikit.Core.Common;

public interface IDynamicValue
{
    object? Deserialize(Type valueType);
}
