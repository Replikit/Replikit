using Microsoft.Extensions.Caching.Memory;

namespace Replikit.Adapters.Common.Extensions;

public static class MemoryCacheExtensions
{
    public static async Task<TItem> GetOrCreateAsync<TKey, TItem>(this IMemoryCache cache, TKey key,
        Func<TKey, CancellationToken, Task<TItem>> resolver,
        TimeSpan? absoluteExpirationRelativeToNow = null,
        CancellationToken cancellationToken = default)
        where TItem : class?
    {
        if (!cache.TryGetValue(key, out var result))
        {
            using var entry = cache.CreateEntry(key);

            result = await resolver(key, cancellationToken).ConfigureAwait(false);
            entry.Value = result;
            entry.AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow;
        }

        return (TItem) result!;
    }
}
