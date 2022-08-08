namespace Replikit.Extensions.Views.Internal;

internal class ViewActionPayload
{
    public ViewActionPayload(int actionIndex)
    {
        ActionIndex = actionIndex;
    }

    public int ActionIndex { get; }
}
