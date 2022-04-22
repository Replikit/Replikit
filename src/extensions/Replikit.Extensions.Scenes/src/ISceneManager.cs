namespace Replikit.Extensions.Scenes;

public interface ISceneManager
{
    Task EnterSceneAsync(SceneRequest sceneRequest, CancellationToken cancellationToken = default);
}
