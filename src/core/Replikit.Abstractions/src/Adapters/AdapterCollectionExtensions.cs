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
    /// <param name="botId">An identifier of the bot to resolve the adapter for.</param>
    /// <returns>A resolved adapter or <c>null</c> if no adapter was found.</returns>
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
    /// <param name="botId">An identifier of the bot to resolve the adapter for.</param>
    /// <returns>A resolved adapter.</returns>
    /// <exception cref="AdapterNotFoundException">The requested adapter was not found.</exception>
    public static IAdapter ResolveRequired(this IAdapterCollection adapterCollection, BotIdentifier botId)
    {
        return adapterCollection.Resolve(botId) ?? throw new AdapterNotFoundException(botId);
    }

    /// <summary>
    /// Resolves an adapter to work with the specified entity.
    /// </summary>
    /// <param name="adapterCollection">The adapter collection.</param>
    /// <param name="entityId">An identifier of the entity to resolve the adapter for.</param>
    /// <returns>A resolved adapter or <c>null</c> if no adapter was found.</returns>
    public static IAdapter? Resolve(this IAdapterCollection adapterCollection, GlobalIdentifier entityId)
    {
        return Resolve(adapterCollection, entityId.BotId);
    }

    /// <summary>
    /// Resolves an adapter to work with the specified entity.
    /// </summary>
    /// <param name="adapterCollection">The adapter collection.</param>
    /// <param name="entityId">An identifier of the entity to resolve the adapter for.</param>
    /// <returns>A resolved adapter.</returns>
    /// <exception cref="AdapterNotFoundException">The requested adapter was not found.</exception>
    public static IAdapter ResolveRequired(this IAdapterCollection adapterCollection, GlobalIdentifier entityId)
    {
        return ResolveRequired(adapterCollection, entityId.BotId);
    }
}
