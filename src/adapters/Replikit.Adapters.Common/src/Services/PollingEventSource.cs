using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Common.Exceptions;
using Replikit.Abstractions.Events;

namespace Replikit.Adapters.Common.Services;

public abstract class PollingEventSource<TUpdate> : EventSource
{
    private CancellationTokenSource? _pollingCancellationTokenSource;

    protected PollingEventSource(IAdapter adapter, IAdapterEventDispatcher eventDispatcher) : base(adapter, eventDispatcher) { }

    protected abstract Task<IEnumerable<TUpdate>?> FetchUpdatesAsync(CancellationToken cancellationToken);
    protected abstract bool ShouldRetryAfterException(Exception exception);
    protected abstract Task HandleUpdatesAsync(IEnumerable<TUpdate> updates, CancellationToken cancellationToken);

    private async Task PollAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            IEnumerable<TUpdate>? updates = null;

            while (updates is null)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                try
                {
                    updates = await FetchUpdatesAsync(cancellationToken);
                }
                catch (Exception exception)
                {
                    if (!ShouldRetryAfterException(exception))
                    {
                        throw;
                    }
                }
            }

            await HandleUpdatesAsync(updates, cancellationToken);
        }
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        if (_pollingCancellationTokenSource is not null)
        {
            throw new ReplikitException("Event source is already started");
        }

        _pollingCancellationTokenSource = new CancellationTokenSource();

        _ = PollAsync(_pollingCancellationTokenSource.Token);

        return Task.CompletedTask;
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        if (_pollingCancellationTokenSource is null)
        {
            throw new ReplikitException("Event source is not started");
        }

        _pollingCancellationTokenSource.Cancel();
        _pollingCancellationTokenSource.Dispose();
        _pollingCancellationTokenSource = null;

        return Task.CompletedTask;
    }
}
