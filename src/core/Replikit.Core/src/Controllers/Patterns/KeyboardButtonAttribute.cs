using Kantaiko.Controllers.Introspection.Factory.Attributes;
using Kantaiko.Controllers.Introspection.Factory.Context;
using Kantaiko.Controllers.Matching;
using Kantaiko.Routing.Events;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Abstractions.Messages.Events;
using Replikit.Core.Localization;

namespace Replikit.Core.Controllers.Patterns;

[AttributeUsage(AttributeTargets.Method)]
public class KeyboardButtonAttribute : Attribute, IEndpointMatcherFactory<IEventContext<MessageReceivedEvent>>
{
    private readonly Type _localeType;
    private readonly string _localeName;

    public KeyboardButtonAttribute(Type localeType, string localeName)
    {
        _localeType = localeType;
        _localeName = localeName;
    }

    public IEndpointMatcher<IEventContext<MessageReceivedEvent>> CreateEndpointMatcher(EndpointFactoryContext context)
    {
        var localizer = context.ServiceProvider.GetRequiredService<ILocalizer>();
        return new KeyboardButtonMatcher(localizer, _localeType, _localeName);
    }
}
