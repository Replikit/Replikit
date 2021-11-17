using Replikit.Abstractions.Common.Models;
using Replikit.Extensions.Common.Scenes;

namespace Replikit.Extensions.Scenes;

public interface ISceneManager
{
    Task EnterScene(GlobalIdentifier channelId, SceneStage stage, CancellationToken cancellationToken = default);

    Task EnterScene(SceneStage stage, CancellationToken cancellationToken = default);
}
