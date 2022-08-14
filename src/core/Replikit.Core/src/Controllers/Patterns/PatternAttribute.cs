using Kantaiko.Controllers.Introspection.Factory.Attributes;
using Kantaiko.Controllers.Introspection.Factory.Context;
using Kantaiko.Controllers.Matching;
using Replikit.Core.Controllers.Context;

namespace Replikit.Core.Controllers.Patterns;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class PatternAttribute : Attribute, IEndpointMatcherFactory<IMessageControllerContext>
{
    private readonly string _pattern;

    public PatternAttribute(string pattern)
    {
        _pattern = pattern;
    }

    public IEndpointMatcher<IMessageControllerContext> CreateEndpointMatcher(EndpointFactoryContext context)
    {
        return new PatternMatcher(_pattern);
    }
}
