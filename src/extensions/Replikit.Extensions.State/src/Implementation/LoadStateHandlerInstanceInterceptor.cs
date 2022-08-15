using Replikit.Core.Routing;

namespace Replikit.Extensions.State.Implementation;

internal class LoadStateHandlerInstanceInterceptor : IHandlerInstanceInterceptor
{
    private readonly IStateLoader _stateLoader;

    public LoadStateHandlerInstanceInterceptor(IStateLoader stateLoader)
    {
        _stateLoader = stateLoader;
    }

    public Task InterceptAsync(object handler, CancellationToken cancellationToken)
    {
        return _stateLoader.LoadAsync(cancellationToken);
    }
}
