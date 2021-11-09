namespace Replikit.Extensions.Common.Utils;

public static class ReadOnlyListExtensions
{
    public static int IndexOf<T>(this IReadOnlyList<T> collection, T element)
    {
        for (var index = 0; index < collection.Count; index++)
        {
            if (Equals(collection[index], element))
            {
                return index;
            }
        }

        return -1;
    }
}
