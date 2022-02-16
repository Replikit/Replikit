namespace Replikit.Extensions.Common.Views;

public static class ViewStorageExtensions
{
    public static IAsyncEnumerable<ViewInstance> GetAllByTypeAsync<TView>(this IViewStorage storage,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(storage);

        return storage.GetAllByTypeAsync(typeof(TView).FullName!, cancellationToken);
    }
}
