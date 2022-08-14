namespace Replikit.Core.Routing;

public interface IHandlerInstanceInterceptor
{
    Task InterceptAsync(object handler, CancellationToken cancellationToken);
}
