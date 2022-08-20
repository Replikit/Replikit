using System.Diagnostics;
using System.Linq.Expressions;
using Kantaiko.Controllers.Introspection;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Events;
using Replikit.Abstractions.Messages.Models;
using Replikit.Core.Routing.Context;
using Replikit.Extensions.State;
using Replikit.Extensions.Views.Actions;
using Replikit.Extensions.Views.Models;

namespace Replikit.Extensions.Views.Internal;

internal class InternalViewContext
{
    public InternalViewContext(EndpointInfo actionEndpoint, IReadOnlyList<object> parameters,
        IState<ViewState> viewState, ViewActionContext viewActionContext)
    {
        ViewController = actionEndpoint.Controller!;
        ActionRequest = new ActionRequest(actionEndpoint, parameters);
        ViewState = viewState;
        ViewActionContext = viewActionContext;

        Debug.Assert(ViewState.Key is not null);

        ViewMessageId = new GlobalMessageIdentifier(
            new GlobalIdentifier(ViewState.Key.BotId!.Value, ViewState.Key.ChannelId!.Value),
            ViewState.Key.MessagePartId!.Value
        );
    }

    public InternalViewContext(ControllerInfo viewController, Expression? action, IState<ViewState> viewState,
        GlobalIdentifier? channelId)
    {
        ViewController = viewController;

        if (action is not null)
        {
            var info = MethodExpressionHelper.ExtractInfo(action);

            var actionEndpoint = viewController.Endpoints.FirstOrDefault(c => c.MethodInfo == info.Method);

            if (actionEndpoint is null)
            {
                throw new InvalidOperationException("Expression must be a view method call");
            }

            ActionRequest = new ActionRequest(actionEndpoint, info.Parameters);
        }

        ViewState = viewState;
        ChannelId = channelId;

        if (ViewState.Key is not null)
        {
            ViewMessageId = new GlobalMessageIdentifier(
                new GlobalIdentifier(ViewState.Key.BotId!.Value, ViewState.Key.ChannelId!.Value),
                ViewState.Key.MessagePartId!.Value
            );
        }
    }

    public ControllerInfo ViewController { get; }
    public ActionRequest? ActionRequest { get; }
    public IState<ViewState> ViewState { get; }
    public GlobalIdentifier? ChannelId { get; }

    public GlobalMessageIdentifier? ViewMessageId { get; set; }

    public ViewActionContext? ViewActionContext { get; }
}
