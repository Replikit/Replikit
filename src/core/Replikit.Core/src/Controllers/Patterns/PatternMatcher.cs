using Kantaiko.Controllers.Matching;
using Kantaiko.Controllers.Matching.Text;
using Kantaiko.Routing.Events;
using Replikit.Abstractions.Messages.Events;

namespace Replikit.Core.Controllers.Patterns;

internal class PatternMatcher : IEndpointMatcher<IEventContext<MessageReceivedEvent>>
{
    private readonly PatternTextMatcher _patternTextMatcher;

    public PatternMatcher(string pattern)
    {
        _patternTextMatcher = new PatternTextMatcher(pattern);
    }

    public EndpointMatchResult Match(EndpointMatchContext<IEventContext<MessageReceivedEvent>> context)
    {
        return context.RequestContext.Event.Message.Text is not null
            ? _patternTextMatcher.Match(context.RequestContext.Event.Message.Text)
            : EndpointMatchResult.NotMatched;
    }
}
