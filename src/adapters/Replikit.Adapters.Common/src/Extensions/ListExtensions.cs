namespace Replikit.Adapters.Common.Extensions;

public static class ListExtensions
{
    public static IEnumerable<List<T>> SplitToChunks<T>(this List<T> items, int chunkSize)
    {
        for (var i = 0; i < items.Count; i += chunkSize)
        {
            yield return items.GetRange(i, Math.Min(chunkSize, items.Count - i));
        }
    }
}
