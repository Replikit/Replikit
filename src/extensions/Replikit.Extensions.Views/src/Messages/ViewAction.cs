using System.Linq.Expressions;

namespace Replikit.Extensions.Views.Messages;

public class ViewAction
{
    internal ViewAction(string text, Expression<Action> action) : this(text, (Expression) action) { }
    internal ViewAction(string text, Expression<Func<Task>> action) : this(text, (Expression) action) { }

    internal ViewAction(string text, Expression action)
    {
        Text = text;
        Action = action;
    }

    public string Text { get; }
    public Expression Action { get; }
}
