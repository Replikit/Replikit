using Microsoft.Extensions.DependencyInjection;

namespace Replikit.Core.Localization;

internal static class ServiceCollectionExtensions
{
    public static void AddLocalizationInternal(this IServiceCollection services)
    {
        services.AddSingleton<ILocalizer, Localizer>();
    }
}
