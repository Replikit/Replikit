using Replikit.Abstractions.Events;
using Replikit.Core.Routing.Context;

namespace Replikit.Core.Routing;

public delegate Task AdapterEventDelegate(IAdapterEventContext<IBotEvent> context);
