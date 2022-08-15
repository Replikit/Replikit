using Replikit.Abstractions.Messages.Events;
using Replikit.Abstractions.Messages.Models;
using Replikit.Adapters.Telegram.Abstractions;
using Replikit.Core.Handlers;
using Replikit.Core.Routing.Context;
using TelegramMessage = Telegram.Bot.Types.Message;

namespace Replikit.Examples.AdapterServices.Handlers;

public class DiceHandler : AdapterEventHandler<MessageReceivedEvent>
{
    public override async Task HandleAsync(IAdapterEventContext<MessageReceivedEvent> context)
    {
        if (context.Adapter is ITelegramAdapter)
        {
            var telegramMessage = context.Event.Message.GetOriginal<TelegramMessage>();

            if (telegramMessage.Dice is { } dice)
            {
                await context.MessageCollection.SendAsync($"Value: {dice.Value}");
            }
        }
    }
}
