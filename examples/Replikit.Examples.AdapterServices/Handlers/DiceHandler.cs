using Replikit.Abstractions.Common.CustomData;
using Replikit.Abstractions.Messages.Events;
using Replikit.Adapters.Telegram.Abstractions;
using Replikit.Core.Handlers;
using Replikit.Core.Routing.Context;
using TelegramMessage = Telegram.Bot.Types.Message;

namespace Replikit.Examples.AdapterServices.Handlers;

public class DiceHandler : BotEventHandler<MessageReceivedEvent>
{
    public override async Task HandleAsync(IBotEventContext<MessageReceivedEvent> context)
    {
        if (context.Adapter is ITelegramAdapter)
        {
            var telegramMessage = context.Event.Message.GetCustomData<TelegramMessage>();

            if (telegramMessage.Dice is { } dice)
            {
                await context.MessageCollection.SendAsync($"Value: {dice.Value}");
            }
        }
    }
}
