using Kantaiko.Controllers;
using Kantaiko.Routing.Events;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Messages.Builder;
using Replikit.Abstractions.Messages.Events;
using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Repositories.Models;
using Replikit.Core.EntityCollections;
using Replikit.Core.Handlers.Extensions;

namespace Replikit.Core.Controllers;

public abstract class Controller : ControllerBase<IEventContext<MessageReceivedEvent>>
{
    protected MessageReceivedEvent Event => Context.Event;
    protected AccountInfo Account => Event.Account;
    protected ChannelInfo Channel => Event.Channel;

    private IMessageCollection? _messageCollection;
    protected IMessageCollection MessageCollection => _messageCollection ??= Context.GetRequiredMessageCollection();

    private IAdapter? _adapter;

    protected IAdapter Adapter => _adapter ??= Context.GetRequiredAdapter();

    protected Message Message => Event.Message;

    protected static MessageBuilder CreateBuilder() => new();
}
