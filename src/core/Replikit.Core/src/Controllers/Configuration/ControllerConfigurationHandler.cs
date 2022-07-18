using Kantaiko.Controllers.Execution;
using Kantaiko.Controllers.Introspection.Factory;
using Kantaiko.Routing.AutoRegistration;
using Kantaiko.Routing.Handlers;
using Replikit.Core.Controllers.Configuration.Context;

namespace Replikit.Core.Controllers.Configuration;

public abstract class ControllerConfigurationHandler<TContext> :
    ContextHandlerBase<IControllerConfigurationContext<TContext>>, IAutoRegistrableHandler
{
    protected IServiceProvider ServiceProvider => Context.ServiceProvider;

    protected IHandlerCollection<TContext> Handlers => Context.Handlers;
    protected IntrospectionBuilder IntrospectionBuilder => Context.IntrospectionBuilder;
}
