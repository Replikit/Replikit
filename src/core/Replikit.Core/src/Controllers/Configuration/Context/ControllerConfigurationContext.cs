using Kantaiko.Controllers.Execution;
using Kantaiko.Controllers.Introspection.Factory;
using Kantaiko.Routing.Context;

namespace Replikit.Core.Controllers.Configuration.Context;

public class ControllerConfigurationContext<TContext> : ContextBase, IControllerConfigurationContext<TContext>
{
    public ControllerConfigurationContext(IntrospectionBuilder introspectionBuilder,
        IHandlerCollection<TContext> handlers, IServiceProvider serviceProvider) : base(serviceProvider)
    {
        IntrospectionBuilder = introspectionBuilder;
        Handlers = handlers;
    }

    public IntrospectionBuilder IntrospectionBuilder { get; }
    public IHandlerCollection<TContext> Handlers { get; }
}
