using Kantaiko.Controllers.Design.Endpoints;
using Kantaiko.Controllers.Design.Properties;
using Kantaiko.Controllers.Matchers;
using Replikit.Abstractions.Messages.Events;
using Replikit.Core.Handlers;

namespace Replikit.Core.Controllers.Patterns;

[AttributeUsage(AttributeTargets.Method)]
public class CommandAttribute : Attribute, IEndpointMatcherFactory<IEventContext<MessageReceivedEvent>>,
    IEndpointDesignPropertyProvider
{
    private readonly string _name;
    private readonly string[] _aliases;

    public CommandAttribute(string name, params string[] aliases)
    {
        _name = name;
        _aliases = new[] { name }.Concat(aliases).ToArray();
    }

    public IEndpointMatcher<IEventContext<MessageReceivedEvent>> CreateEndpointMatcher(EndpointDesignContext context)
    {
        return CommandMatcherFactory.CreateEndpointMatcher(context, _aliases);
    }

    public DesignPropertyCollection GetEndpointDesignProperties() => new()
    {
        [ReplikitEndpointProperties.CommandName] = _name
    };
}
