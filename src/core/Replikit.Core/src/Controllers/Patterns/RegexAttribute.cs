using System.Text.RegularExpressions;
using Kantaiko.Controllers.Introspection.Factory.Attributes;
using Kantaiko.Controllers.Introspection.Factory.Context;
using Kantaiko.Controllers.Matching;
using Kantaiko.Routing.Events;
using Replikit.Abstractions.Messages.Events;

namespace Replikit.Core.Controllers.Patterns;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class RegexAttribute : Attribute, IEndpointMatcherFactory<IEventContext<MessageReceivedEvent>>
{
    private readonly string _pattern;
    private readonly RegexOptions _regexOptions;

    public RegexAttribute(string pattern, RegexOptions regexOptions = RegexOptions.None)
    {
        _pattern = pattern;
        _regexOptions = regexOptions;
    }

    public IEndpointMatcher<IEventContext<MessageReceivedEvent>> CreateEndpointMatcher(EndpointFactoryContext context)
    {
        return new RegexMatcher(_pattern, _regexOptions);
    }
}
