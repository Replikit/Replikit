using Replikit.Abstractions.Messages.Events;
using Replikit.Core.Handlers.Context;

namespace Replikit.Core.Controllers.Context;

public interface IMessageControllerContext : IChannelEventContext<MessageReceivedEvent> { }
