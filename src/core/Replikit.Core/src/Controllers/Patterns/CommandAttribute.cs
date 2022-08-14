using Kantaiko.Controllers.Introspection.Factory.Attributes;
using Kantaiko.Controllers.Introspection.Factory.Context;
using Kantaiko.Controllers.Matching;
using Kantaiko.Properties.Immutable;
using Replikit.Core.Controllers.Context;

namespace Replikit.Core.Controllers.Patterns;

[AttributeUsage(AttributeTargets.Method)]
public class CommandAttribute : Attribute, IEndpointMatcherFactory<IMessageControllerContext>,
    IEndpointPropertyProvider
{
    private readonly string _name;
    private readonly string[] _aliases;

    public CommandAttribute(string name, params string[] aliases)
    {
        _name = name;
        _aliases = new[] { name }.Concat(aliases).ToArray();
    }

    public IEndpointMatcher<IMessageControllerContext> CreateEndpointMatcher(EndpointFactoryContext context)
    {
        return CommandMatcherFactory.CreateEndpointMatcher(context, _aliases);
    }

    public IImmutablePropertyCollection UpdateEndpointProperties(EndpointFactoryContext context)
    {
        return context.Endpoint.Properties.Set(new CommandEndpointProperties(_name, _aliases));
    }
}
