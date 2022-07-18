using Kantaiko.Controllers.Matching;
using Kantaiko.Controllers.Matching.Text;
using Kantaiko.Routing.Events;
using Replikit.Abstractions.Messages.Events;
using Replikit.Core.Controllers.Context;

namespace Replikit.Core.Controllers.Patterns;

internal class PatternMatcher : IEndpointMatcher<IMessageControllerContext>
{
    private readonly PatternTextMatcher _patternTextMatcher;

    public PatternMatcher(string pattern)
    {
        _patternTextMatcher = new PatternTextMatcher(pattern);
    }

    public EndpointMatchResult Match(EndpointMatchContext<IMessageControllerContext> context)
    {
        return context.RequestContext.Event.Message.Text is not null
            ? _patternTextMatcher.Match(context.RequestContext.Event.Message.Text)
            : EndpointMatchResult.NotMatched;
    }
}
