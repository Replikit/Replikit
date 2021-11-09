using System.Collections.Immutable;
using Kantaiko.Controllers.Introspection;
using Kantaiko.Controllers.Matchers;

namespace Replikit.Extensions.Scenes.Internal;

public class StageEndpointMatcher : IEndpointMatcher<SceneContext>
{
    private readonly string _type;
    private readonly string _method;

    public StageEndpointMatcher(EndpointInfo endpointInfo)
    {
        _type = endpointInfo.MethodInfo.DeclaringType!.FullName!;
        _method = endpointInfo.MethodInfo.ToString()!;
    }

    public EndpointMatchResult Match(EndpointMatchContext<SceneContext> context)
    {
        if (context.RequestContext.Request.Stage.SceneType != _type)
        {
            return EndpointMatchResult.NotMatched;
        }

        if (context.RequestContext.Request.Stage.Method != _method)
        {
            return EndpointMatchResult.NotMatched;
        }

        return EndpointMatchResult.Success(ImmutableDictionary<string, string>.Empty);
    }
}
