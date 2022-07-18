using Kantaiko.Routing.Context;
using Replikit.Abstractions.Events;
using Replikit.Core.Handlers.Context;

namespace Replikit.Core.Services;

public class CancellationTokenProvider : ContextService, ICancellationTokenProvider
{
    public CancellationTokenProvider(ContextAccessor<IAdapterEventContext<IAdapterEvent>> contextAccessor) :
        base(contextAccessor) { }

    CancellationToken ICancellationTokenProvider.CancellationToken => CancellationToken;
}
