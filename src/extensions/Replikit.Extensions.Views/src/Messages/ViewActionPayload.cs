namespace Replikit.Extensions.Views.Messages;

public class ViewActionPayload
{
    public ViewActionPayload(int actionIndex)
    {
        ActionIndex = actionIndex;
    }

    public int ActionIndex { get; }
}
