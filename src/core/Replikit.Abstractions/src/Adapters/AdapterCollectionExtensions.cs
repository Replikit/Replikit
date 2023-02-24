using Replikit.Abstractions.Adapters.Exceptions;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Common.Utilities;

namespace Replikit.Abstractions.Adapters;

/// <summary>
/// The extensions for <see cref="IAdapterCollection"/>.
/// </summary>
public static class AdapterCollectionExtensions
{
    /// <summary>
    /// Resolves an adapter for the specified bot.
    /// </summary>
    /// <param name="adapterCollection">The adapter collection.</param>
    /// <param name="botId">The identifier of the bot to resolve the adapter for.</param>
    /// <returns>The resolved adapter or <c>null</c> if no adapter was found.</returns>
    public static IAdapter? Resolve(this IAdapterCollection adapterCollection, BotIdentifier botId)
    {
        Check.NotNull(adapterCollection);
        Check.NotDefault(botId);

        return adapterCollection.GetAdapters().FirstOrDefault(x => x.BotInfo.Id == botId);
    }

    /// <summary>
    /// Resolves an adapter for the specified bot.
    /// </summary>
    /// <param name="adapterCollection">The adapter collection.</param>
    /// <param name="botId">The identifier of the bot to resolve the adapter for.</param>
    /// <returns>The resolved adapter.</returns>
    /// <exception cref="AdapterNotFoundException">The requested adapter was not found.</exception>
    public static IAdapter ResolveRequired(this IAdapterCollection adapterCollection, BotIdentifier botId)
    {
        return adapterCollection.Resolve(botId) ?? throw new AdapterNotFoundException(botId);
    }

    /// <summary>
    /// Resolves an adapter by the platform identifier.
    /// If there are multiple adapters with the same platform identifier, the first one will be returned.
    /// </summary>
    /// <param name="adapterCollection">The adapter collection.</param>
    /// <param name="platformId">The platform identifier.</param>
    /// <returns>The resolved adapter or <c>null</c> if no adapter was found.</returns>
    public static IAdapter? Resolve(this IAdapterCollection adapterCollection, string platformId)
    {
        Check.NotNull(adapterCollection);
        Check.NotNull(platformId);

        return adapterCollection.GetAdapters().FirstOrDefault(x => x.BotInfo.Id.PlatformId == platformId);
    }

    /// <summary>
    /// Resolves an adapter by the platform identifier.
    /// </summary>
    /// <param name="adapterCollection">The adapter collection.</param>
    /// <param name="platformId">The platform identifier.</param>
    /// <returns>The resolved adapter.</returns>
    /// <exception cref="AdapterNotFoundException">The requested adapter was not found.</exception>
    public static IAdapter ResolveRequired(this IAdapterCollection adapterCollection, string platformId)
    {
        return adapterCollection.Resolve(platformId) ?? throw new AdapterNotFoundException(platformId);
    }

    /// <summary>
    /// Resolves the default adapter (the first one in the collection).
    /// </summary>
    /// <param name="collection">The adapter collection.</param>
    /// <returns>The resolved adapter or <c>null</c> if no adapter was found.</returns>
    public static IAdapter? Resolve(this IAdapterCollection collection)
    {
#pragma warning disable CA1826
        return collection.GetAdapters().FirstOrDefault();
#pragma warning restore CA1826
    }

    /// <summary>
    /// Resolves the default adapter (the first one in the collection).
    /// </summary>
    /// <param name="collection">The adapter collection.</param>
    /// <returns>The resolved adapter.</returns>
    /// <exception cref="AdapterNotFoundException">The requested adapter was not found.</exception>
    public static IAdapter ResolveRequired(this IAdapterCollection collection)
    {
        return collection.Resolve() ?? throw new AdapterNotFoundException();
    }

    /// <summary>
    /// Resolves an adapter to work with the specified entity.
    /// </summary>
    /// <param name="adapterCollection">The adapter collection.</param>
    /// <param name="entityId">The identifier of the entity to resolve the adapter for.</param>
    /// <returns>The resolved adapter or <c>null</c> if no adapter was found.</returns>
    public static IAdapter? Resolve(this IAdapterCollection adapterCollection, GlobalIdentifier entityId)
    {
        Check.NotNull(adapterCollection);
        Check.NotDefault(entityId);

        if (entityId.BotId is { } botId)
        {
            return Resolve(adapterCollection, botId);
        }

        return entityId.PlatformId is not null
            ? Resolve(adapterCollection, entityId.PlatformId)
            : Resolve(adapterCollection);
    }

    /// <summary>
    /// Resolves an adapter to work with the specified entity.
    /// </summary>
    /// <param name="adapterCollection">The adapter collection.</param>
    /// <param name="entityId">The identifier of the entity to resolve the adapter for.</param>
    /// <returns>The resolved adapter.</returns>
    /// <exception cref="AdapterNotFoundException">The requested adapter was not found.</exception>
    public static IAdapter ResolveRequired(this IAdapterCollection adapterCollection, GlobalIdentifier entityId)
    {
        Check.NotNull(adapterCollection);
        Check.NotDefault(entityId);

        if (entityId.BotId is { } botId)
        {
            return ResolveRequired(adapterCollection, botId);
        }

        return entityId.PlatformId is not null
            ? ResolveRequired(adapterCollection, entityId.PlatformId)
            : ResolveRequired(adapterCollection);
    }
}
