using Kantaiko.Controllers.Execution;
using Kantaiko.Controllers.Introspection.Factory;
using Replikit.Core.Controllers.Context;

namespace Replikit.Core.Controllers.Configuration;

public class ControllerConfigurationBuilder
{
    public ControllerConfigurationBuilder(IntrospectionBuilder introspectionBuilder,
        IHandlerCollection<IMessageControllerContext> handlers)
    {
        Handlers = handlers;
        IntrospectionBuilder = introspectionBuilder;
    }

    public IntrospectionBuilder IntrospectionBuilder { get; }
    public IHandlerCollection<IMessageControllerContext> Handlers { get; }
}
