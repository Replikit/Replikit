using Replikit.Abstractions.Adapters.Factory;
using Replikit.Adapters.Common.Adapters;
using Telegram.Bot;

namespace Replikit.Adapters.Telegram;

public class TelegramAdapterFactory : AdapterFactory<TelegramAdapterOptions>
{
    protected override Task<Adapter> CreateAsync(TelegramAdapterOptions options, AdapterFactoryContext context,
        CancellationToken cancellationToken = default)
    {
        var backend = new TelegramBotClient(options.Token);
        var adapter = new TelegramAdapter(context, backend, options);

        return Task.FromResult<Adapter>(adapter);
    }
}
