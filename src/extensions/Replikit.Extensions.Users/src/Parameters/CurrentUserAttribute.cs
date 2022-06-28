using Kantaiko.Controllers.Introspection;
using Kantaiko.Controllers.Introspection.Factory.Attributes;
using Kantaiko.Controllers.Introspection.Factory.Context;
using Kantaiko.Controllers.ParameterConversion.Text;
using Kantaiko.Properties.Immutable;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Abstractions.Common.Exceptions;
using Replikit.Core.Abstractions.Users;

namespace Replikit.Extensions.Users.Parameters;

[AttributeUsage(AttributeTargets.Parameter)]
public class CurrentUserAttribute : Attribute, IParameterPropertyProvider, ITextParameterConverterFactoryProvider
{
    public IImmutablePropertyCollection UpdateParameterProperties(ParameterFactoryContext context)
    {
        return context.Parameter.Properties
            .Update<VisibilityParameterProperties>(props => props with { IsHidden = true });
    }

    public Func<IServiceProvider, ITextParameterConverter> GetTextParameterConverterFactory(
        ParameterFactoryContext context)
    {
        var parameterType = context.Parameter.ParameterType;

        if (GetReplikitUserIdType(parameterType) is not { } idType)
        {
            throw new ReplikitException(
                "[CurrentUser] attribute can only be used on types that are compatible with ReplikitUser");
        }

        var converterType = typeof(CurrentUserConverter<,>).MakeGenericType(parameterType, idType);

        return sp => (ITextParameterConverter) sp.GetRequiredService(converterType);
    }

    private static Type? GetReplikitUserIdType(Type? type)
    {
        while (type is not null)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ReplikitUser<>))
            {
                return type.GetGenericArguments()[0];
            }

            type = type.BaseType;
        }

        return null;
    }
}
