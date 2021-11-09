namespace Replikit.Core.Services;

public interface ICancellationTokenProvider
{
    CancellationToken CancellationToken { get; }
}
