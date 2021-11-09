using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Common.Models;
using Replikit.Adapters.Common.Adapters;
using Replikit.Adapters.Telegram.Exceptions;
using Telegram.Bot;

namespace Replikit.Adapters.Telegram;

public class TelegramAdapterFactory : AdapterFactory<TelegramAdapterOptions>
{
    public const string Type = "tg";

    protected override IAdapter Create(TelegramAdapterOptions options, AdapterContext context)
    {
        var client = new TelegramBotClient(options.Token);

        if (!client.BotId.HasValue)
        {
            throw new TelegramAdapterException("Invalid bot token provided");
        }

        var adapterId = new AdapterIdentifier(Type, client.BotId);
        return new TelegramAdapter(adapterId, context, client, options);
    }
}
