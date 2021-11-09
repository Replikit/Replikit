namespace Replikit.Adapters.Common.Extensions;

public static class EnumExtensions
{
    public static IEnumerable<T> GetFlags<T>(this T e) where T : Enum
    {
        return Enum.GetValues(e.GetType()).Cast<T>().Where(x => e.HasFlag(x));
    }
}
