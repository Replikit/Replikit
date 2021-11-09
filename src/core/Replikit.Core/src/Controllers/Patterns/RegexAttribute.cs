using System.Text.RegularExpressions;
using Kantaiko.Controllers.Design.Endpoints;
using Kantaiko.Controllers.Matchers;
using Replikit.Abstractions.Messages.Events;
using Replikit.Core.Handlers;

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

    public IEndpointMatcher<IEventContext<MessageReceivedEvent>> CreateEndpointMatcher(EndpointDesignContext context)
    {
        return new RegexMatcher(_pattern, _regexOptions);
    }
}
