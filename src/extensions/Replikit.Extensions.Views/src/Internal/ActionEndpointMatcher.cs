using Kantaiko.Controllers.Introspection;
using Kantaiko.Controllers.Matching;
using Kantaiko.Properties.Immutable;

namespace Replikit.Extensions.Views.Internal;

internal class ActionEndpointMatcher : IEndpointMatcher<ViewContext>
{
    private readonly EndpointInfo _endpointInfo;

    public ActionEndpointMatcher(EndpointInfo endpointInfo)
    {
        _endpointInfo = endpointInfo;
    }

    public EndpointMatchResult Match(EndpointMatchContext<ViewContext> context)
    {
        if (context.RequestContext.Request.Type != _endpointInfo.Controller!.Type.FullName)
        {
            return EndpointMatchResult.NotMatched;
        }

        if (context.RequestContext.Request.Method != _endpointInfo.MethodInfo.ToString())
        {
            return EndpointMatchResult.NotMatched;
        }

        return EndpointMatchResult.Success(ImmutablePropertyCollection.Empty);
    }
}
