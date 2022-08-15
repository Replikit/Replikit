namespace Replikit.Core.Common;

public static class DynamicValueExtensions
{
    public static T GetValue<T>(this IDynamicValue dynamicValue) => (T) dynamicValue.Deserialize(typeof(T))!;
}
