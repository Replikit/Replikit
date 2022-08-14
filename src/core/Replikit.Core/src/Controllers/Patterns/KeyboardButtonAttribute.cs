using Kantaiko.Controllers.Introspection.Factory.Attributes;
using Kantaiko.Controllers.Introspection.Factory.Context;
using Kantaiko.Controllers.Matching;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Controllers.Context;
using Replikit.Core.Localization;

namespace Replikit.Core.Controllers.Patterns;

[AttributeUsage(AttributeTargets.Method)]
public class KeyboardButtonAttribute : Attribute, IEndpointMatcherFactory<IMessageControllerContext>
{
    private readonly Type _localeType;
    private readonly string _localeName;

    public KeyboardButtonAttribute(Type localeType, string localeName)
    {
        _localeType = localeType;
        _localeName = localeName;
    }

    public IEndpointMatcher<IMessageControllerContext> CreateEndpointMatcher(EndpointFactoryContext context)
    {
        var localizer = context.ServiceProvider.GetRequiredService<ILocalizer>();
        return new KeyboardButtonMatcher(localizer, _localeType, _localeName);
    }
}
