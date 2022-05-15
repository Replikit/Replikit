namespace Replikit.Core.Common;

public static class DynamicValueExtensions
{
    public static T GetValue<T>(this DynamicValue dynamicValue) => (T) dynamicValue.GetValue(typeof(T))!;
}
