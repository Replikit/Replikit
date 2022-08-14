using System.Collections.Concurrent;
using System.Globalization;
using System.Resources;

namespace Replikit.Core.Localization;

internal class Localizer : ILocalizer
{
    private readonly ConcurrentDictionary<Type, ResourceManager> _resourceManagers = new();

    private static ResourceManager CreateResourceManager(Type type) => new(type);

    public string Localize(Type localeType, string localeName, CultureInfo? cultureInfo)
    {
        ArgumentNullException.ThrowIfNull(localeType);
        ArgumentNullException.ThrowIfNull(localeName);

        var resourceManager = _resourceManagers.GetOrAdd(localeType, CreateResourceManager);
        return resourceManager.GetString(localeName, cultureInfo) ?? localeName;
    }
}
