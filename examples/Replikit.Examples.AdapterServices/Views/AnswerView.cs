using Replikit.Abstractions.Messages.Builder;
using Replikit.Abstractions.Messages.Events;
using Replikit.Adapters.Telegram.Abstractions;
using Replikit.Core.Handlers.Context;
using Replikit.Extensions.Views;
using Telegram.Bot;

namespace Replikit.Examples.AdapterServices.Views;

public class AnswerView : View
{
    public override Task<ViewResult> RenderAsync(CancellationToken cancellationToken)
    {
        var message = ViewResult.CreateBuilder()
            .AddText("Test")
            .AddAction("Test", c => Reply(c));

        return Task.FromResult<ViewResult>(message);
    }

    [Action(AutoUpdate = false)]
    public void Reply(IAdapterEventContext<ButtonPressedEvent> eventContext)
    {
        if (eventContext.Adapter is ITelegramAdapter adapter && eventContext.Event.RequestId is { } requestId)
        {
            adapter.Backend.AnswerCallbackQueryAsync(requestId, "No");
        }
    }
}
