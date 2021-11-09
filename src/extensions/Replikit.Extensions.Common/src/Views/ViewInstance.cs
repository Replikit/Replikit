using Replikit.Abstractions.Messages.Models;
using Replikit.Extensions.Common.Models;

namespace Replikit.Extensions.Common.Views;

public class ViewInstance
{
    private ViewInstance() { }

    public ViewInstance(GlobalMessageIdentifier messageId, string type, DynamicValue state,
        IReadOnlyList<ViewInstanceAction>? actions)
    {
        MessageId = messageId;
        Type = type;
        State = state;
        Actions = actions;
    }

    public GlobalMessageIdentifier MessageId { get; } = null!;
    public string Type { get; private set; } = null!;
    public DynamicValue State { get; private set; } = null!;
    public IReadOnlyList<ViewInstanceAction>? Actions { get; private set; } = null!;
}
