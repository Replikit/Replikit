using Kantaiko.Routing;
using Kantaiko.Routing.Events;
using Replikit.Abstractions.Events;

namespace Replikit.Core.Handlers;

public interface IAdapterEventRouter
{
    IHandler<IEventContext<Event>, Task<Unit>> Handler { get; set; }
}
