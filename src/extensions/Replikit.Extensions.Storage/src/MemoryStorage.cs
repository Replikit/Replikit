namespace Replikit.Extensions.Storage;

internal class MemoryStorage<TKey, TValue> : IStorage<TKey, TValue> where TKey : notnull where TValue : notnull
{
    private readonly Dictionary<TKey, TValue> _values = new();

    public IAsyncQueryable<KeyValuePair<TKey, TValue>> CreateQuery()
    {
        lock (_values)
        {
            return _values.ToAsyncEnumerable().AsAsyncQueryable();
        }
    }

    public Task SetManyAsync(IEnumerable<KeyValuePair<TKey, TValue?>> valuePairs,
        CancellationToken cancellationToken = default)
    {
        lock (_values)
        {
            foreach (var (key, value) in valuePairs)
            {
                if (value is not null)
                {
                    _values[key] = value;
                }
                else
                {
                    _values.Remove(key);
                }
            }
        }

        return Task.CompletedTask;
    }
}
