using System.Collections.Concurrent;
using Replikit.Abstractions.Common.Models;
using Replikit.Extensions.Common.Scenes;

namespace Replikit.Extensions.Scenes.Internal;

internal class MemorySceneStorage : ISceneStorage
{
    public const string Name = "memory";

    private readonly ConcurrentDictionary<Identifier, SceneInstance> _sceneInstances = new();

    public Task<SceneInstance?> GetAsync(Identifier channelId, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_sceneInstances.GetValueOrDefault(channelId));
    }

    public Task SetAsync(Identifier channelId, SceneInstance sceneInstance,
        CancellationToken cancellationToken = default)
    {
        _sceneInstances[channelId] = sceneInstance;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Identifier channelId, CancellationToken cancellationToken = default)
    {
        _sceneInstances.Remove(channelId, out _);
        return Task.CompletedTask;
    }
}
