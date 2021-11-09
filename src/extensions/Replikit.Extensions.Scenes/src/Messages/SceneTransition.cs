using System.Linq.Expressions;

namespace Replikit.Extensions.Scenes.Messages;

public class SceneTransition
{
    public SceneTransition(string text, Expression<Func<Task>> stage) : this(text, (Expression) stage) { }

    public SceneTransition(string text, Expression<Action> stage) : this(text, (Expression) stage) { }

    internal SceneTransition(string text, Expression stage)
    {
        Text = text;
        Stage = stage;
    }

    public string Text { get; }
    public Expression Stage { get; }
}

public class SceneTransition<TScene> : SceneTransition where TScene : Scene
{
    public SceneTransition(string text, Expression<Func<TScene, Task>> stage) : base(text, stage) { }

    public SceneTransition(string text, Expression<Action<TScene>> stage) : base(text, stage) { }
}
