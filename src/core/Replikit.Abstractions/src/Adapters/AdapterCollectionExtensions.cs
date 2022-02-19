using Replikit.Abstractions.Adapters.Exceptions;
using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Adapters;

public static class AdapterCollectionExtensions
{
    public static IAdapter ResolveRequired(this IAdapterCollection adapterCollection, string name)
    {
        return adapterCollection.Resolve(name) ?? throw new AdapterNotFoundException(name);
    }

    public static IAdapter ResolveRequired(this IAdapterCollection adapterCollection, AdapterIdentifier identifier)
    {
        return adapterCollection.Resolve(identifier) ?? throw new AdapterNotFoundException(identifier);
    }

    public static IAdapter ResolveRequired(this IAdapterCollection adapterCollection, GlobalIdentifier identifier)
    {
        ArgumentNullException.ThrowIfNull(identifier);

        return adapterCollection.ResolveRequired(identifier.AdapterId);
    }
}
