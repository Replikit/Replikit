using Kantaiko.Controllers.Introspection.Factory.Attributes;
using Kantaiko.Controllers.Introspection.Factory.Context;
using Kantaiko.Controllers.Matching;
using Kantaiko.Properties.Immutable;
using Replikit.Extensions.Views.Internal;

namespace Replikit.Extensions.Views;

[AttributeUsage(AttributeTargets.Method)]
public class ActionAttribute : Attribute, IEndpointMatcherFactory<ViewContext>, IEndpointPropertyProvider
{
    public bool AllowExternalActivation { get; init; }

    public IEndpointMatcher<ViewContext> CreateEndpointMatcher(EndpointFactoryContext context)
    {
        return new ActionEndpointMatcher(context.Endpoint);
    }

    public IImmutablePropertyCollection UpdateEndpointProperties(EndpointFactoryContext context)
    {
        return context.Endpoint.Properties.Set(new ViewActionEndpointProperties(true, AllowExternalActivation));
    }
}
