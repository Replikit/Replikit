using System.Collections.Immutable;

namespace Replikit.Extensions.Storage;

public class StorageOptions
{
    public IImmutableSet<StorageDefinition> StorageDefinitions { get; set; } =
        ImmutableHashSet<StorageDefinition>.Empty;

    public void RegisterStorageDefinition(string name, Type keyType, Type valueType)
    {
        StorageDefinitions = StorageDefinitions.Add(new StorageDefinition(name, keyType, valueType));
    }
}
