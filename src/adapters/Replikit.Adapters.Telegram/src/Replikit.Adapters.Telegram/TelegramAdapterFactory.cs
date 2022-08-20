using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Adapters.Factory;
using Replikit.Adapters.Common.Adapters;
using Telegram.Bot;

namespace Replikit.Adapters.Telegram;

public class TelegramAdapterFactory : AdapterFactory<TelegramAdapterOptions>
{
    protected override AdapterInfo AdapterInfo => new(TelegramAdapter.Type, "Telegram Bot API");

    protected override PlatformInfo PlatformInfo => new("telegram", "Telegram");

    protected override Task<Adapter> CreateAsync(TelegramAdapterOptions options,
        AdapterInfo adapterInfo, PlatformInfo platformInfo,
        AdapterFactoryContext context, CancellationToken cancellationToken = default)
    {
        var backend = new TelegramBotClient(options.Token);
        var adapter = new TelegramAdapter(adapterInfo, platformInfo, context, backend, options);

        return Task.FromResult<Adapter>(adapter);
    }
}
