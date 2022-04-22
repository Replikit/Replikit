using System.Linq.Expressions;

namespace Replikit.Extensions.Views.Messages;

public class ViewMessageAction
{
    internal ViewMessageAction(string text, Expression action)
    {
        Text = text;
        Action = action;
    }

    public string Text { get; }
    public Expression Action { get; }
}
