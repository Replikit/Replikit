namespace Replikit.Core.Hosting.Abstractions;

public interface IReplikitCoreLifetime
{
    event Func<CancellationToken, Task> AdaptersInitialized;
}
