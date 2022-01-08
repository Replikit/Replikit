using Kantaiko.Routing;
using Kantaiko.Routing.Events;
using Replikit.Core.Hosting.Events;

namespace Replikit.Core.Hosting;

public interface IReplikitLifecycle
{
    IHandler<IEventContext<AdaptersInitializedEvent>, Task<Unit>> AdaptersInitialized { get; set; }
}
