namespace Replikit.Extensions.Common.Views;

public class ViewInstanceAction
{
    private ViewInstanceAction() { }

    public ViewInstanceAction(string method, object[] parameters)
    {
        Method = method;
        Parameters = parameters;
    }

    public string Method { get; private set; } = null!;
    public object[] Parameters { get; private set; } = null!;
}
