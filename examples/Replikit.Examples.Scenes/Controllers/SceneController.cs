using Replikit.Abstractions.Common.Models;
using Replikit.Core.Controllers;
using Replikit.Core.Controllers.Patterns;
using Replikit.Examples.Scenes.Scenes;
using Replikit.Extensions.Scenes;

namespace Replikit.Examples.Scenes.Controllers;

public class SceneController : Controller
{
    private readonly ISceneManager _sceneManager;

    public SceneController(ISceneManager sceneManager)
    {
        _sceneManager = sceneManager;
    }

    [Command("scene")]
    public Task EnterScene(long channelId)
    {
        var globalId = new GlobalIdentifier(Adapter.Id, channelId);

        return _sceneManager.EnterSceneAsync<MainScene>(globalId, scene => scene.MainStage());
    }

    [Command("scene")]
    public Task EnterScene()
    {
        return _sceneManager.EnterSceneAsync<MainScene>(Channel.Id, scene => scene.MainStage());
    }
}
