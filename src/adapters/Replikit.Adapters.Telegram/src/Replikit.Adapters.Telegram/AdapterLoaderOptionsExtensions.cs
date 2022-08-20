using Replikit.Abstractions.Adapters.Loader;

namespace Replikit.Adapters.Telegram;

public static class AdapterLoaderOptionsExtensions
{
    public static void AddTelegram(this IAdapterLoaderOptions loaderOptions)
    {
        ArgumentNullException.ThrowIfNull(loaderOptions);

        loaderOptions.RegisterFactory(TelegramAdapter.Type, new TelegramAdapterFactory());
    }

    public static void AddTelegram(this IAdapterLoaderOptions loaderOptions, TelegramAdapterOptions options)
    {
        ArgumentNullException.ThrowIfNull(loaderOptions);
        ArgumentNullException.ThrowIfNull(options);

        loaderOptions.RegisterFactory(TelegramAdapter.Type, new TelegramAdapterFactory());
        loaderOptions.AddDescriptor(new AdapterDescriptor(TelegramAdapter.Type, options));
    }

    public static void AddTelegram(this IAdapterLoaderOptions loaderOptions, string token)
    {
        ArgumentNullException.ThrowIfNull(loaderOptions);
        ArgumentNullException.ThrowIfNull(token);

        loaderOptions.AddTelegram(new TelegramAdapterOptions { Token = token });
    }
}
