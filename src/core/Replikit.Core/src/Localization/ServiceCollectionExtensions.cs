using Microsoft.Extensions.DependencyInjection;

namespace Replikit.Core.Localization;

public static class ServiceCollectionExtensions
{
    public static void AddReplikitLocalization(this IServiceCollection services)
    {
        services.AddSingleton<ILocalizer, Localizer>();
    }
}
