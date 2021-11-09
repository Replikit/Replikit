# Adapter services

- [Getting started](getting-started.md)
- [Controllers](controllers.md)
- [Messages](messages.md)
- [Routing](routing.md)
- Adapter services
- [Further reading](further-reading.md)

Up to this point, we have only looked at the scenario when the bot responds to an incoming message. However, the API of
modern platforms allows you to do much more interesting things. As you might have guessed, an adapter is what is
responsible for interacting with a specific platform. In addition, it should be said that all adapters have the same
interface and are divided into subsystems called `features`.

All features provided by the framework are available as properties of the adapter instance. The current adapter is
available inside the controller. Let's create a simple command to edit the message sent by the bot:

```c#
public class EditController : Controller
{
    [Command("edit")]
    public async Task<OutMessage?> Edit(string text)
    {
        if (Message.Reply is null || Message.Reply.AccountId != Adapter.Id.BotId)
        {
            return "You should reply to a bot message";
        }

        await Adapter.MessageService.EditAsync(Message.Reply.ChannelId!, Message.Reply.Id, text);

        return "Message edited";
    }
}
```

Here we check if there is a reply to the message, as well as that the message belongs to the bot by comparing the sender
ID with the `BotId` property of the adapter identifier.

However, not all platforms support post editing. In this case, the method is still available, but may throw an exception
on some platforms. To avoid this we can use the `Supports` method:

```c#
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
```

<!-- TODO Add docs about other services -->
<!-- TODO Add docs about MessageCollection -->

Besides the MessageService, many other features are also available. The documentation will be updated.

Read more: [Futher reading](further-reading.md)
