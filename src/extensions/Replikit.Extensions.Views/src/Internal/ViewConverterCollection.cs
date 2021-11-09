using System.Collections.Immutable;
using Kantaiko.Controllers.Converters;

namespace Replikit.Extensions.Views.Internal;

internal class ViewConverterCollection : IConverterCollection
{
    public Type ResolveConverterType(Type parameterType) => typeof(ViewParameterConverter);

    public IReadOnlyDictionary<Type, Type> ConverterTypes => ImmutableDictionary<Type, Type>.Empty;
}
