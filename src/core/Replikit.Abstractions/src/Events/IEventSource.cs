namespace Replikit.Abstractions.Events;

public interface IEventSource
{
    Task StartAsync(CancellationToken cancellationToken = default);
    Task StopAsync(CancellationToken cancellationToken = default);
}
