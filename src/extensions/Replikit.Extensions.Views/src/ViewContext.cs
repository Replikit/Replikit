using Kantaiko.Properties;
using Kantaiko.Routing.Context;

namespace Replikit.Extensions.Views;

public class ViewContext : ContextBase
{
    public ViewContext(ViewRequest request,
        IServiceProvider? serviceProvider = null,
        IReadOnlyPropertyCollection? properties = null,
        CancellationToken cancellationToken = default) :
        base(serviceProvider, properties, cancellationToken)
    {
        Request = request;
    }

    public ViewRequest Request { get; }
}
