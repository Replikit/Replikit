using Kantaiko.Controllers;

namespace Replikit.Extensions.Scenes.Internal;

internal class SceneRequestHandlerAccessor
{
    public RequestHandler<SceneContext> RequestHandler { get; set; } = null!;
}
