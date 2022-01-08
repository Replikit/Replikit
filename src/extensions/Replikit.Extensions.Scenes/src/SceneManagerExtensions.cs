using System.Linq.Expressions;
using Replikit.Abstractions.Common.Models;
using Replikit.Extensions.Common.Scenes;
using Replikit.Extensions.Common.Utils;

namespace Replikit.Extensions.Scenes;

public static class SceneManagerExtensions
{
    public static Task EnterSceneAsync<TScene>(this ISceneManager sceneManager, GlobalIdentifier channelId,
        Expression<Action<TScene>> expression, CancellationToken cancellationToken = default) where TScene : Scene =>
        EnterSceneInternalAsync<TScene>(sceneManager, channelId, expression, cancellationToken);

    public static Task EnterSceneAsync<TScene>(this ISceneManager sceneManager, GlobalIdentifier channelId,
        Expression<Func<TScene, Task>> expression, CancellationToken cancellationToken = default)
        where TScene : Scene =>
        EnterSceneInternalAsync<TScene>(sceneManager, channelId, expression, cancellationToken);

    public static Task EnterSceneAsync<TScene>(this ISceneManager sceneManager,
        Expression<Action<TScene>> expression, CancellationToken cancellationToken = default) where TScene : Scene =>
        EnterSceneInternalAsync<TScene>(sceneManager, expression, cancellationToken);

    public static Task EnterSceneAsync<TScene>(this ISceneManager sceneManager,
        Expression<Func<TScene, Task>> expression, CancellationToken cancellationToken = default)
        where TScene : Scene =>
        EnterSceneInternalAsync<TScene>(sceneManager, expression, cancellationToken);

    private static Task EnterSceneInternalAsync<TScene>(this ISceneManager sceneManager,
        GlobalIdentifier channelId, Expression expression,
        CancellationToken cancellationToken = default) where TScene : Scene
    {
        var (methodInfo, parameters) = MethodExpressionTransformer.Transform(expression);
        var stage = new SceneStage(typeof(TScene).FullName!, methodInfo.ToString()!, parameters);

        return sceneManager.EnterSceneAsync(channelId, stage, cancellationToken);
    }

    private static Task EnterSceneInternalAsync<TScene>(this ISceneManager sceneManager, Expression expression,
        CancellationToken cancellationToken = default) where TScene : Scene
    {
        var (methodInfo, parameters) = MethodExpressionTransformer.Transform(expression);
        var stage = new SceneStage(typeof(TScene).FullName!, methodInfo.ToString()!, parameters);

        return sceneManager.EnterSceneAsync(stage, cancellationToken);
    }
}
