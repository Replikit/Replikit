using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Messages.Services;
using Replikit.Core.Controllers;
using Replikit.Core.Controllers.Patterns;
using Replikit.Core.Requirements;

namespace Replikit.Examples.AdapterServices.Controllers;

public class EditController : Controller
{
    [Command("edit")]
    [RequireFeatures(MessageServiceFeatures.Edit)]
    public async Task<OutMessage?> Edit(string text)
    {
        if (Message.Reply is null || Message.Reply.AccountId != Adapter.Id.BotId)
        {
            return "You should reply to a bot message";
        }

        await MessageCollection.EditAsync(Message.Reply.Id, text);

        return "Message edited";
    }
}
