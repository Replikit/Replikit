using Kantaiko.Controllers.Introspection;

namespace Replikit.Extensions.Scenes;

public interface ISceneIntrospectionInfoAccessor
{
    IntrospectionInfo IntrospectionInfo { get; }
}
