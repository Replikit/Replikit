using Kantaiko.Controllers.Design.Endpoints;
using Kantaiko.Controllers.Design.Properties;
using Kantaiko.Controllers.Matchers;
using Replikit.Extensions.Views.Internal;

namespace Replikit.Extensions.Views;

[AttributeUsage(AttributeTargets.Method)]
public class ActionAttribute : Attribute, IEndpointMatcherFactory<ViewContext>,
    IEndpointDesignPropertyProvider
{
    public bool AllowExternalActivation { get; init; }

    public IEndpointMatcher<ViewContext> CreateEndpointMatcher(EndpointDesignContext context)
    {
        return new ActionEndpointMatcher(context.Info);
    }

    public DesignPropertyCollection GetEndpointDesignProperties() => new()
    {
        [ViewEndpointProperties.IsAction] = true,
        [ViewEndpointProperties.AllowExternalActivation] = AllowExternalActivation
    };
}
