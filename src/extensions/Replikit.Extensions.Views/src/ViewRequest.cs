using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Events;
using Replikit.Extensions.State;
using Replikit.Extensions.Storage.Models;
using Replikit.Extensions.Views.Models;

namespace Replikit.Extensions.Views;

public class ViewRequest
{
    public ViewRequest(string type, string method,
        IReadOnlyList<DynamicValue> parameters,
        IState<ViewState>? viewState = null,
        ButtonPressedEvent? @event = null,
        GlobalIdentifier? channelId = null)
    {
        Type = type;
        ViewState = viewState;
        Method = method;
        Parameters = parameters;
        ChannelId = channelId;
        Event = @event;
    }

    public string Type { get; }
    public string Method { get; }
    public IReadOnlyList<DynamicValue> Parameters { get; }

    public IState<ViewState>? ViewState { get; }
    public ButtonPressedEvent? Event { get; }
    public GlobalIdentifier? ChannelId { get; }
}
