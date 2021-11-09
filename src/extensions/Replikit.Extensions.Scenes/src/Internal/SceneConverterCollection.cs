using System.Collections.Immutable;
using Kantaiko.Controllers.Converters;

namespace Replikit.Extensions.Scenes.Internal;

internal class SceneConverterCollection : IConverterCollection
{
    public Type ResolveConverterType(Type parameterType) => typeof(SceneParameterConverter);

    public IReadOnlyDictionary<Type, Type> ConverterTypes => ImmutableDictionary<Type, Type>.Empty;
}
