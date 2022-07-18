using Replikit.Abstractions.Messages.Builder;
using Replikit.Adapters.Telegram.Abstractions;
using Replikit.Extensions.Views;
using Telegram.Bot;

namespace Replikit.Examples.AdapterServices.Views;

public class AnswerView : View
{
    public override Task<ViewResult> RenderAsync(CancellationToken cancellationToken)
    {
        var message = CreateBuilder()
            .AddText("Test")
            .AddAction("Test", () => Reply());

        return Task.FromResult<ViewResult>(message);
    }

    [Action(AutoUpdate = false)]
    public void Reply()
    {
        if (Adapter is ITelegramAdapter adapter && AdapterEvent.RequestId is not null)
        {
            adapter.Backend.AnswerCallbackQueryAsync(AdapterEvent.RequestId, "No");
        }
    }
}
