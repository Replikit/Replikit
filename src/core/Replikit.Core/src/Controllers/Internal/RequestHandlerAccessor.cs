using Kantaiko.Controllers;
using Replikit.Abstractions.Messages.Events;
using Replikit.Core.Handlers;

namespace Replikit.Core.Controllers.Internal;

internal class RequestHandlerAccessor
{
    public RequestHandler<IEventContext<MessageReceivedEvent>> RequestHandler { get; set; } = null!;
}
