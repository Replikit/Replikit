using Replikit.Abstractions.Adapters.Loader;
using Replikit.Adapters.Common.Adapters;

namespace Replikit.Adapters.Telegram;

public static class AdapterLoaderOptionsExtensions
{
    public static void AddTelegram(this IAdapterLoaderOptions loaderOptions)
    {
        ArgumentNullException.ThrowIfNull(loaderOptions);

        loaderOptions.EnsureTelegramAdapterFactoryConfigured();
    }

    public static void AddTelegram(this IAdapterLoaderOptions loaderOptions, TelegramAdapterOptions options)
    {
        ArgumentNullException.ThrowIfNull(loaderOptions);
        ArgumentNullException.ThrowIfNull(options);

        loaderOptions.EnsureTelegramAdapterFactoryConfigured();
        loaderOptions.AdapterDescriptors.Add(new AdapterDescriptor(TelegramAdapterFactory.Type, options));
    }

    public static void AddTelegram(this IAdapterLoaderOptions loaderOptions, string token)
    {
        ArgumentNullException.ThrowIfNull(loaderOptions);
        ArgumentNullException.ThrowIfNull(token);

        loaderOptions.AddTelegram(new TelegramAdapterOptions { Token = token });
    }

    private static void EnsureTelegramAdapterFactoryConfigured(this IAdapterLoaderOptions loaderOptions)
    {
        loaderOptions.EnsureAdapterFactoryConfigured(TelegramAdapterFactory.Type, () => new TelegramAdapterFactory());
    }
}
