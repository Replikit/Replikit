using Replikit.Abstractions.Common.Models;
using Replikit.Extensions.Common.Scenes;

namespace Replikit.Extensions.Scenes;

public interface ISceneManager
{
    Task EnterSceneAsync(GlobalIdentifier channelId, SceneStage stage, CancellationToken cancellationToken = default);

    Task EnterSceneAsync(SceneStage stage, CancellationToken cancellationToken = default);
}
