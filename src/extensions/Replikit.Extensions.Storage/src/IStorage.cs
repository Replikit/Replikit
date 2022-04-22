namespace Replikit.Extensions.Storage;

public interface IStorage<TKey, TValue> where TKey : notnull where TValue : notnull
{
    IAsyncQueryable<KeyValuePair<TKey, TValue>> CreateQuery();

    Task SetManyAsync(IEnumerable<KeyValuePair<TKey, TValue?>> valuePairs,
        CancellationToken cancellationToken = default);
}
