using Replikit.Abstractions.Messages.Events;
using Replikit.Core.Routing.Context;

namespace Replikit.Core.Controllers.Context;

public interface IMessageControllerContext : IBotEventContext<MessageReceivedEvent> { }
