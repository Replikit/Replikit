using Replikit.Extensions.Common.Scenes;

namespace Replikit.Extensions.Scenes;

public interface ISceneManager
{
    Task EnterScene(SceneStage stage, CancellationToken cancellationToken = default);
}
