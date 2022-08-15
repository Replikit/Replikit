using Replikit.Adapters.Telegram.Abstractions;
using Replikit.Extensions.Views;
using Replikit.Extensions.Views.Actions;
using Telegram.Bot;

namespace Replikit.Examples.AdapterServices.Views;

public class AnswerView : View
{
    public override ViewMessage Render() => new()
    {
        Text = "Test",
        Actions =
        {
            Action("Can I have a cookie?", c => Reply(c))
        }
    };

    public void Reply(IViewActionContext context)
    {
        context.SuppressAutoUpdate();

        if (context.Adapter is ITelegramAdapter adapter && context.Event.RequestId is { } requestId)
        {
            adapter.Backend.AnswerCallbackQueryAsync(requestId, "No");
        }
    }
}
