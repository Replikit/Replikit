using Kantaiko.Routing;
using Kantaiko.Routing.Events;

namespace Replikit.Core.Controllers.Lifecycle;

public interface IControllerLifecycle
{
    IHandler<IEventContext<ControllerInstantiatedEvent>, Task> ControllerInstantiated { get; set; }
}
