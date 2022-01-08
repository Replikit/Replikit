using Kantaiko.Controllers.Introspection.Factory.Attributes;
using Kantaiko.Controllers.Introspection.Factory.Context;
using Kantaiko.Controllers.Matching;
using Replikit.Extensions.Scenes.Internal;

namespace Replikit.Extensions.Scenes;

[AttributeUsage(AttributeTargets.Method)]
public class StageAttribute : Attribute, IEndpointMatcherFactory<SceneContext>
{
    public IEndpointMatcher<SceneContext> CreateEndpointMatcher(EndpointFactoryContext context)
    {
        return new StageEndpointMatcher(context.Endpoint);
    }
}
