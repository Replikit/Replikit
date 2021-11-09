using System.Text.RegularExpressions;
using Kantaiko.Controllers.Matchers;
using Replikit.Abstractions.Messages.Events;
using Replikit.Core.Handlers;

namespace Replikit.Core.Controllers.Patterns;

internal class RegexMatcher : IEndpointMatcher<IEventContext<MessageReceivedEvent>>
{
    private readonly RegexTextMatcher _regexTextMatcher;

    public RegexMatcher(string pattern, RegexOptions regexOptions = RegexOptions.None)
    {
        _regexTextMatcher = new RegexTextMatcher(pattern, regexOptions);
    }

    public EndpointMatchResult Match(EndpointMatchContext<IEventContext<MessageReceivedEvent>> context)
    {
        return context.RequestContext.Event.Message.Text is not null
            ? _regexTextMatcher.Match(context.RequestContext.Event.Message.Text)
            : EndpointMatchResult.NotMatched;
    }
}
