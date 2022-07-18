using Microsoft.Extensions.DependencyInjection;

namespace Replikit.Extensions.Scenes.Internal;

internal class SceneManager : ISceneManager
{
    private readonly SceneHandlerAccessor _handlerAccessor;
    private readonly IServiceProvider _serviceProvider;

    public SceneManager(SceneHandlerAccessor handlerAccessor, IServiceProvider serviceProvider)
    {
        _handlerAccessor = handlerAccessor;
        _serviceProvider = serviceProvider;
    }

    public async Task EnterSceneAsync(SceneRequest sceneRequest, CancellationToken cancellationToken = default)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();

        var sceneContext = new SceneContext(sceneRequest, scope.ServiceProvider, cancellationToken);

        await _handlerAccessor.Handler.HandleAsync(sceneContext, scope.ServiceProvider, cancellationToken);
    }
}
