using Kantaiko.Routing.Events;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Events;
using Replikit.Core.Common;
using Replikit.Core.Handlers.Context;
using Replikit.Extensions.State;
using Replikit.Extensions.Views.Models;

namespace Replikit.Extensions.Views;

public class ViewRequest
{
    public ViewRequest(string type, string method,
        IReadOnlyList<DynamicValue> parameters,
        IState<ViewState>? viewState = null,
        GlobalIdentifier? channelId = null)
    {
        Type = type;
        ViewState = viewState;
        Method = method;
        Parameters = parameters;
        ChannelId = channelId;
    }

    public string Type { get; }
    public string Method { get; }
    public IReadOnlyList<DynamicValue> Parameters { get; }

    public IState<ViewState>? ViewState { get; }
    public GlobalIdentifier? ChannelId { get; }
}
