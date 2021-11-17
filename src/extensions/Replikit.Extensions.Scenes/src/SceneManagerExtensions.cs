using System.Linq.Expressions;
using Replikit.Abstractions.Common.Models;
using Replikit.Core.EntityCollections;
using Replikit.Extensions.Common.Scenes;
using Replikit.Extensions.Common.Utils;

namespace Replikit.Extensions.Scenes;

public static class SceneManagerExtensions
{
    public static Task EnterScene<TScene>(this ISceneManager sceneManager, GlobalIdentifier channelId,
        Expression<Action<TScene>> expression, CancellationToken cancellationToken = default) where TScene : Scene =>
        EnterSceneInternal<TScene>(sceneManager, channelId, expression, cancellationToken);

    public static Task EnterScene<TScene>(this ISceneManager sceneManager, GlobalIdentifier channelId,
        Expression<Func<TScene, Task>> expression, CancellationToken cancellationToken = default)
        where TScene : Scene =>
        EnterSceneInternal<TScene>(sceneManager, channelId, expression, cancellationToken);

    public static Task EnterScene<TScene>(this ISceneManager sceneManager,
        Expression<Action<TScene>> expression, CancellationToken cancellationToken = default) where TScene : Scene =>
        EnterSceneInternal<TScene>(sceneManager, expression, cancellationToken);

    public static Task EnterScene<TScene>(this ISceneManager sceneManager,
        Expression<Func<TScene, Task>> expression, CancellationToken cancellationToken = default)
        where TScene : Scene =>
        EnterSceneInternal<TScene>(sceneManager, expression, cancellationToken);

    private static Task EnterSceneInternal<TScene>(this ISceneManager sceneManager,
        GlobalIdentifier channelId, Expression expression,
        CancellationToken cancellationToken = default) where TScene : Scene
    {
        var (methodInfo, parameters) = MethodExpressionTransformer.Transform(expression);
        var stage = new SceneStage(typeof(TScene).FullName!, methodInfo.ToString()!, parameters);

        return sceneManager.EnterScene(channelId, stage, cancellationToken);
    }

    private static Task EnterSceneInternal<TScene>(this ISceneManager sceneManager, Expression expression,
        CancellationToken cancellationToken = default) where TScene : Scene
    {
        var (methodInfo, parameters) = MethodExpressionTransformer.Transform(expression);
        var stage = new SceneStage(typeof(TScene).FullName!, methodInfo.ToString()!, parameters);

        return sceneManager.EnterScene(stage, cancellationToken);
    }
}
