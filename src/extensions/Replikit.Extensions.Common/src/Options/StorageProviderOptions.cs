namespace Replikit.Extensions.Common.Options;

public class StorageProviderOptions<TStorage>
{
    public string? DefaultType { get; set; }

    public Dictionary<string, Type> StorageTypes { get; } = new();
}
