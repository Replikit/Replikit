using Kantaiko.Controllers.Design.Endpoints;
using Kantaiko.Controllers.Matchers;
using Replikit.Abstractions.Messages.Events;
using Replikit.Core.Handlers;

namespace Replikit.Core.Controllers.Patterns;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class PatternAttribute : Attribute, IEndpointMatcherFactory<IEventContext<MessageReceivedEvent>>
{
    private readonly string _pattern;

    public PatternAttribute(string pattern)
    {
        _pattern = pattern;
    }

    public IEndpointMatcher<IEventContext<MessageReceivedEvent>> CreateEndpointMatcher(EndpointDesignContext context)
    {
        return new PatternMatcher(_pattern);
    }
}
