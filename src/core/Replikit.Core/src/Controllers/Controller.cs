using Kantaiko.Controllers;
using Replikit.Abstractions.Accounts.Models;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Channels.Models;
using Replikit.Abstractions.Messages.Events;
using Replikit.Abstractions.Messages.Models;
using Replikit.Core.Controllers.Context;
using Replikit.Core.EntityCollections;

namespace Replikit.Core.Controllers;

public abstract class Controller : ControllerBase<IMessageControllerContext>
{
    protected MessageReceivedEvent Event => Context.Event;

    protected AccountInfo Account => Event.Account;
    protected ChannelInfo Channel => Event.Channel;
    protected Message Message => Event.Message;

    protected IMessageCollection MessageCollection => Context.MessageCollection;
    protected IAdapter Adapter => Context.Adapter;
}
