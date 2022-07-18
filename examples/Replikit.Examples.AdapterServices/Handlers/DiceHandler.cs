using Kantaiko.Routing;
using Replikit.Abstractions.Messages.Events;
using Replikit.Adapters.Telegram.Abstractions;
using Replikit.Core.Handlers;
using Replikit.Core.Handlers.Context;
using TelegramMessage = Telegram.Bot.Types.Message;

namespace Replikit.Examples.AdapterServices.Handlers;

public class DiceHandler : MessageEventHandler<MessageReceivedEvent>
{
    protected override async Task<Unit> HandleAsync(IChannelEventContext<MessageReceivedEvent> context, NextAction next)
    {
        if (Adapter is ITelegramAdapter)
        {
            var telegramMessage = Message.GetOriginal<TelegramMessage>();

            if (telegramMessage.Dice is { } dice)
            {
                await MessageCollection.SendAsync($"Value: {dice.Value}");
            }
        }

        return await next();
    }
}
