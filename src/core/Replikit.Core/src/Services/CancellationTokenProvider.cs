using Replikit.Core.Handlers;

namespace Replikit.Core.Services;

public class CancellationTokenProvider : ICancellationTokenProvider
{
    private readonly IEventContextAccessor _eventContextAccessor;

    public CancellationTokenProvider(IEventContextAccessor eventContextAccessor)
    {
        _eventContextAccessor = eventContextAccessor;
    }

    public CancellationToken CancellationToken =>
        _eventContextAccessor.Context?.CancellationToken ?? CancellationToken.None;
}
