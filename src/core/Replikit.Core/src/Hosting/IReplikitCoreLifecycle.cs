using Kantaiko.Routing.Events;
using Replikit.Core.Hosting.Events;

namespace Replikit.Core.Hosting;

public interface IReplikitCoreLifecycle
{
    event AsyncEventHandler<IAsyncEventContext<AdaptersInitializedEvent>> AdaptersInitialized;
}
