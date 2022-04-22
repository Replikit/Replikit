using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Common.Models;
using Replikit.Adapters.Common.Adapters;
using Telegram.Bot;

namespace Replikit.Adapters.Telegram;

public class TelegramAdapterFactory : AdapterFactory<TelegramAdapterOptions>
{
    public const string Type = "tg";

    protected override string DisplayName => "Telegram";

    protected override Task<Adapter> CreateAsync(TelegramAdapterOptions options, AdapterFactoryContext context,
        CancellationToken cancellationToken = default)
    {
        var backend = new TelegramBotClient(options.Token);
        var adapterId = new AdapterIdentifier(Type, backend.BotId!.Value);

        var adapter = new TelegramAdapter(adapterId, context, backend, options);

        return Task.FromResult<Adapter>(adapter);
    }
}
