using Kantaiko.Controllers.Design.Endpoints;
using Kantaiko.Controllers.Matchers;
using Replikit.Extensions.Scenes.Internal;

namespace Replikit.Extensions.Scenes;

[AttributeUsage(AttributeTargets.Method)]
public class StageAttribute : Attribute, IEndpointMatcherFactory<SceneContext>
{
    public IEndpointMatcher<SceneContext> CreateEndpointMatcher(EndpointDesignContext context)
    {
        return new StageEndpointMatcher(context.Info);
    }
}
