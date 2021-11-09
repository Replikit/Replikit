using Replikit.Abstractions.Messages.Events;
using Replikit.Core.EntityCollections;
using Replikit.Extensions.Common.Views;

namespace Replikit.Extensions.Views;

public class ViewRequest
{
    public ViewRequest(string type, string method, object[] parameters,
        ViewInstance? viewInstance = null,
        ButtonPressedEvent? @event = null,
        IMessageCollection? messageCollection = null,
        bool autoSave = true)
    {
        Type = type;
        ViewInstance = viewInstance;
        Method = method;
        Parameters = parameters;
        Event = @event;
        MessageCollection = messageCollection;
        AutoSave = autoSave;
    }

    public string Type { get; }
    public string Method { get; }
    public object[] Parameters { get; }

    public ViewInstance? ViewInstance { get; }
    public ButtonPressedEvent? Event { get; }
    public IMessageCollection? MessageCollection { get; }
    public bool AutoSave { get; }
}
