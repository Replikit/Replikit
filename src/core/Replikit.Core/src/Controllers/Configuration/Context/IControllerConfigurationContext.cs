using Kantaiko.Controllers.Execution;
using Kantaiko.Controllers.Introspection.Factory;
using Kantaiko.Routing.Context;

namespace Replikit.Core.Controllers.Configuration.Context;

public interface IControllerConfigurationContext<TContext> : IContext
{
    IntrospectionBuilder IntrospectionBuilder { get; }
    IHandlerCollection<TContext> Handlers { get; }
}
