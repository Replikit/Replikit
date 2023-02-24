using System.Text.Json;
using Replikit.Abstractions.Common.Models;
using Replikit.Core.Serialization.Json.Converters;

namespace Replikit.Core.Serialization.Json;

/// <summary>
/// The extension class for <see cref="JsonSerializerOptions"/>.
/// </summary>
public static class JsonSerializerOptionsExtensions
{
    /// <summary>
    /// Adds Replikit converters to the <see cref="JsonSerializerOptions"/>.
    /// </summary>
    /// <param name="options">The <see cref="JsonSerializerOptions"/> instance.</param>
    /// <param name="persistenceMode">The <see cref="IdentifierPersistenceMode"/> value.</param>
    public static void AddReplikitConverters(this JsonSerializerOptions options,
        IdentifierPersistenceMode persistenceMode)
    {
        ArgumentNullException.ThrowIfNull(options);

        options.Converters.Add(new DynamicValueConverter());
        options.Converters.Add(new CultureInfoConverter());
        options.Converters.Add(new IdentifierConverter());
        options.Converters.Add(new GlobalIdentifierConverter(persistenceMode));
        options.Converters.Add(new BotIdentifierConverter());
        options.Converters.Add(new MessageIdentifierConverter());
        options.Converters.Add(new GlobalMessageIdentifierSerializer());
        options.Converters.Add(new ReplikitButtonModelConverter());

        options.TypeInfoResolver = new ReplikitTypeInfoResolver();
    }
}
