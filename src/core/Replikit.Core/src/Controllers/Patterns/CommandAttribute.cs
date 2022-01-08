using Kantaiko.Controllers.Introspection.Factory.Attributes;
using Kantaiko.Controllers.Introspection.Factory.Context;
using Kantaiko.Controllers.Matching;
using Kantaiko.Properties.Immutable;
using Kantaiko.Routing.Events;
using Replikit.Abstractions.Messages.Events;

namespace Replikit.Core.Controllers.Patterns;

[AttributeUsage(AttributeTargets.Method)]
public class CommandAttribute : Attribute, IEndpointMatcherFactory<IEventContext<MessageReceivedEvent>>,
    IEndpointPropertyProvider
{
    private readonly string _name;
    private readonly string[] _aliases;

    public CommandAttribute(string name, params string[] aliases)
    {
        _name = name;
        _aliases = new[] { name }.Concat(aliases).ToArray();
    }

    public IEndpointMatcher<IEventContext<MessageReceivedEvent>> CreateEndpointMatcher(EndpointFactoryContext context)
    {
        return CommandMatcherFactory.CreateEndpointMatcher(context, _aliases);
    }

    public IImmutablePropertyCollection UpdateEndpointProperties(EndpointFactoryContext context)
    {
        return context.Endpoint.Properties.Set(new CommandEndpointProperties(_name, _aliases));
    }
}
