using Kantaiko.Routing.Context;

namespace Replikit.Extensions.Views;

public class ViewContext : ContextBase
{
    public ViewContext(ViewRequest request, IServiceProvider serviceProvider,
        CancellationToken cancellationToken) : base(serviceProvider,
        cancellationToken)
    {
        Request = request;
    }

    public ViewRequest Request { get; }
}
