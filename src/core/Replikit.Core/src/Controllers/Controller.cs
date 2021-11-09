using Kantaiko.Controllers;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Messages.Builder;
using Replikit.Abstractions.Messages.Events;
using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Repositories.Models;
using Replikit.Core.EntityCollections;
using Replikit.Core.Handlers;
using Replikit.Core.Handlers.Extensions;

namespace Replikit.Core.Controllers;

public abstract class Controller : ControllerBase<IEventContext<MessageReceivedEvent>>
{
    protected MessageReceivedEvent Event => Context.Event;
    protected AccountInfo Account => Event.Account;
    protected ChannelInfo Channel => Event.Channel;
    protected IMessageCollection MessageCollection => Context.GetMessageCollection();
    protected IAdapter Adapter => Context.Adapter;
    protected Message Message => Event.Message;
    protected IServiceProvider ServiceProvider => Context.ServiceProvider;
    protected CancellationToken CancellationToken => Context.CancellationToken;

    protected static MessageBuilder CreateBuilder() => new();
}
