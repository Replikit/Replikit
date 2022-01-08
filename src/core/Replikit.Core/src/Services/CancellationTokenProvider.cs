using Kantaiko.Routing.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Replikit.Core.Services;

public class CancellationTokenProvider : ICancellationTokenProvider
{
    private readonly IServiceProvider _serviceProvider;

    public CancellationTokenProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public CancellationToken CancellationToken =>
        _serviceProvider.GetService<IContext>()?.CancellationToken ?? CancellationToken.None;
}
