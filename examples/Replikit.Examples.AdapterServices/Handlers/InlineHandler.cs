using Replikit.Abstractions.Common.CustomData;
using Replikit.Abstractions.Events;
using Replikit.Adapters.Telegram.Abstractions;
using Replikit.Core.Handlers;
using Replikit.Core.Routing.Context;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineQueryResults;

namespace Replikit.Examples.AdapterServices.Handlers;

public class InlineHandler : BotEventHandler<UnknownBotEvent>
{
    public override async Task HandleAsync(IBotEventContext<UnknownBotEvent> context)
    {
        if (context.Adapter is not ITelegramAdapter telegramAdapter)
        {
            return;
        }

        if (context.Event.GetCustomDataOrDefault<Update>() is not { InlineQuery: { } inlineQuery })
        {
            return;
        }

        await telegramAdapter.Backend.AnswerInlineQueryAsync(inlineQuery.Id, new[]
        {
            new InlineQueryResultArticle("1", "Test", new InputTextMessageContent("Test"))
        });

        context.IsHandled = true;
    }
}
