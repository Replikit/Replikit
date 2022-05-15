using System.Linq.Expressions;
using Replikit.Abstractions.Common.Models;
using Replikit.Core.Common;
using Replikit.Core.Utils;
using Replikit.Extensions.Scenes.Models;

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

    private static Task EnterSceneInternalAsync<TScene>(this ISceneManager sceneManager,
        GlobalIdentifier channelId, Expression expression,
        CancellationToken cancellationToken = default) where TScene : Scene
    {
        var (methodInfo, parameters) = MethodExpressionTransformer.Transform(expression);

        var dynamicParameters = parameters.Select(x => new DynamicValue(x)).ToArray();

        var stage = new SceneInstanceStage(typeof(TScene).FullName!, methodInfo.ToString()!, dynamicParameters);
        var request = new SceneRequest(channelId, stage, true);

        return sceneManager.EnterSceneAsync(request, cancellationToken);
    }
}
