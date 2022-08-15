using System.Linq.Expressions;

namespace Replikit.Extensions.Views.Actions;

public class ViewAction
{
    public ViewAction(string text, Expression<Action<IViewActionContext>> action)
    {
        ArgumentNullException.ThrowIfNull(text);
        ArgumentNullException.ThrowIfNull(action);

        Text = text;
        Action = action;
    }

    public ViewAction(string text, Expression<Func<IViewActionContext, object>> action)
    {
        ArgumentNullException.ThrowIfNull(text);
        ArgumentNullException.ThrowIfNull(action);

        Text = text;
        Action = action;
    }

    public string Text { get; }
    public Expression Action { get; }
}
