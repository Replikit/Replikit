using Replikit.Abstractions.Common.Features;
using Replikit.Abstractions.Messages.Features;
using Replikit.Abstractions.Messages.Models;
using Replikit.Core.Controllers;
using Replikit.Core.Controllers.Patterns;

namespace Replikit.Examples.AdapterServices.Controllers;

public class EditController : Controller
{
    [Command("edit")]
    public async Task<OutMessage?> Edit(string text)
    {
        if (Message.Reply is null || Message.Reply.AccountId != Adapter.Id.BotId)
        {
            return "You should reply to a bot message";
        }

        if (!Adapter.MessageService.Supports(MessageServiceFeatures.Edit))
        {
            return "Current platform does not support message editing";
        }

        await Adapter.MessageService.EditAsync(Message.Reply.ChannelId!, Message.Reply.Id, text);

        return "Message edited";
    }
}
