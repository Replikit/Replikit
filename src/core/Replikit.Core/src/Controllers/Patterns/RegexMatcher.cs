using System.Text.RegularExpressions;
using Kantaiko.Controllers.Matching;
using Kantaiko.Controllers.Matching.Text;
using Replikit.Core.Controllers.Context;

namespace Replikit.Core.Controllers.Patterns;

internal class RegexMatcher : IEndpointMatcher<IMessageControllerContext>
{
    private readonly RegexTextMatcher _regexTextMatcher;

    public RegexMatcher(string pattern, RegexOptions regexOptions = RegexOptions.None)
    {
        _regexTextMatcher = new RegexTextMatcher(pattern, regexOptions);
    }

    public EndpointMatchResult Match(EndpointMatchContext<IMessageControllerContext> context)
    {
        return context.RequestContext.Event.Message.Text is not null
            ? _regexTextMatcher.Match(context.RequestContext.Event.Message.Text)
            : EndpointMatchResult.NotMatched;
    }
}
