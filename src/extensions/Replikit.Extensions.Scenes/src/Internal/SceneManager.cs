using Kantaiko.Properties.Immutable;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Extensions.State.Context;

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

        var sceneContext = CreateContext(sceneRequest, scope.ServiceProvider, cancellationToken);

        await _handlerAccessor.Handler.Handle(sceneContext);
    }

    public static SceneContext CreateContext(SceneRequest request, IServiceProvider serviceProvider,
        CancellationToken cancellationToken)
    {
        var properties = ImmutablePropertyCollection.Empty
            .Set(new StateContextProperties(SceneStateKeyFactory.Instance));

        return new SceneContext(request, serviceProvider, properties, cancellationToken);
    }
}
