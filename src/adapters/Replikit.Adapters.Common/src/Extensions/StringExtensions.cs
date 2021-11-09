namespace Replikit.Adapters.Common.Extensions;

public static class StringExtensions
{
    public static string Slice(this string str, int startIndex, int endIndex)
    {
        return str.Substring(startIndex, endIndex - startIndex);
    }
}
