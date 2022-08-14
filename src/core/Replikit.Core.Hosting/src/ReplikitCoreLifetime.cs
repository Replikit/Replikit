using Replikit.Core.Hosting.Abstractions;

namespace Replikit.Core.Hosting;

internal class ReplikitCoreLifetime : IReplikitCoreLifetime
{
    public event Func<CancellationToken, Task>? AdaptersInitialized;

    public async Task OnAdaptersInitializedAsync(CancellationToken cancellationToken)
    {
        var handlers = AdaptersInitialized?.GetInvocationList()
            .Cast<Func<CancellationToken, Task>>();

        if (handlers is null)
        {
            return;
        }

        foreach (var handler in handlers)
        {
            await handler(cancellationToken);
        }
    }
}
