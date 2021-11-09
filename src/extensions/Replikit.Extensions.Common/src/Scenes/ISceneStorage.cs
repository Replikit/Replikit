using Replikit.Abstractions.Common.Models;

namespace Replikit.Extensions.Common.Scenes;

public interface ISceneStorage
{
    Task<SceneInstance?> GetAsync(Identifier channelId, CancellationToken cancellationToken = default);
    Task SetAsync(Identifier channelId, SceneInstance sceneInstance, CancellationToken cancellationToken = default);
    Task DeleteAsync(Identifier channelId, CancellationToken cancellationToken = default);
}
