using System.Collections.Concurrent;
using System.Globalization;
using System.Text.RegularExpressions;
using Kantaiko.Controllers.Matching;
using Kantaiko.Controllers.Matching.Text;
using Kantaiko.Routing.Events;
using Replikit.Abstractions.Messages.Events;
using Replikit.Abstractions.Repositories.Events;
using Replikit.Core.Controllers.Context;
using Replikit.Core.Localization;

namespace Replikit.Core.Controllers.Patterns;

public class KeyboardButtonMatcher : IEndpointMatcher<IMessageControllerContext>
{
    private readonly ILocalizer _localizer;
    private readonly Type _localeType;
    private readonly string _localeName;

    private readonly ConcurrentDictionary<CultureInfo, RegexTextMatcher> _textMatchers = new();

    public KeyboardButtonMatcher(ILocalizer localizer, Type localeType, string localeName)
    {
        _localizer = localizer;
        _localeType = localeType;
        _localeName = localeName;
    }

    public EndpointMatchResult Match(EndpointMatchContext<IMessageControllerContext> context)
    {
        var messageText = context.RequestContext.Event.Message.Text;
        if (string.IsNullOrEmpty(messageText)) return EndpointMatchResult.NotMatched;

        var culture = context.RequestContext.Event is IAccountEvent accountEvent
            ? accountEvent.Account.CultureInfo
            : CultureInfo.DefaultThreadCurrentUICulture;

        culture ??= CultureInfo.InvariantCulture;

        var textMatcher = _textMatchers.GetOrAdd(culture, CreateTextMatcher);
        return textMatcher.Match(messageText);
    }

    private RegexTextMatcher CreateTextMatcher(CultureInfo cultureInfo)
    {
        var pattern = _localizer.Localize(_localeType, _localeName);
        return new RegexTextMatcher($"^{Regex.Escape(pattern)}$");
    }
}
