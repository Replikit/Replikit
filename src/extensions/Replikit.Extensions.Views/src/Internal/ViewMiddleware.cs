using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Replikit.Abstractions.Events;
using Replikit.Abstractions.Messages.Events;
using Replikit.Core.Routing;
using Replikit.Core.Routing.Context;
using Replikit.Core.Routing.Middleware;
using Replikit.Extensions.State;
using Replikit.Extensions.State.Implementation;
using Replikit.Extensions.Views.Actions;
using Replikit.Extensions.Views.Models;

namespace Replikit.Extensions.Views.Internal;

internal class ViewMiddleware : IAdapterEventMiddleware
{
    private readonly ILogger<ViewMiddleware> _logger;
    private readonly ViewManager _viewManager;

    public ViewMiddleware(ILogger<ViewMiddleware> logger, ViewManager viewManager)
    {
        _logger = logger;
        _viewManager = viewManager;
    }

    public async Task HandleAsync(IAdapterEventContext<IBotEvent> context, AdapterEventDelegate next)
    {
        if (context is not IAdapterEventContext<ButtonPressedEvent> buttonContext)
        {
            await next(context);
            return;
        }

        if (buttonContext.Event.Message is null)
        {
            _logger.LogDebug("Button press has been skipped because of inaccessible message");

            await next(context);
            return;
        }

        ViewActionPayload? payload;

        try
        {
            if (string.IsNullOrEmpty(buttonContext.Event.Payload)) throw new JsonException();

            payload = JsonSerializer.Deserialize<ViewActionPayload>(buttonContext.Event.Payload);
            if (payload is null) throw new JsonException();
        }
        catch (JsonException exception)
        {
            _logger.LogDebug(exception, "Button press has been skipped because of invalid payload");

            await next(context);
            return;
        }

        var stateManager = context.ServiceProvider.GetRequiredService<IStateManager>();

        var viewState = await stateManager.GetViewStateAsync<ViewState>(
            buttonContext.Event.Message.Id, context.CancellationToken);

        var viewInstance = viewState.Value.ViewInstance;

        if (viewInstance is null)
        {
            _logger.LogDebug("Button press has been skipped because view was not found");

            await next(context);
            return;
        }

        if (viewInstance.Actions is not { Count: > 0 })
        {
            _logger.LogDebug("Button press has been skipped because view contains no actions");

            await next(context);
            return;
        }

        if (payload.ActionIndex < 0 || payload.ActionIndex >= viewInstance.Actions.Count)
        {
            _logger.LogDebug("Button press has been skipped because of invalid index");

            await next(context);
            return;
        }

        var (method, parameters) = viewInstance.Actions[payload.ActionIndex];

        if (!_viewManager.ViewHandlers.TryGetValue(viewInstance.Type, out var viewHandler))
        {
            _logger.LogDebug("Button press has been skipped because of missing view handler");

            await next(context);
            return;
        }

        var endpoint = viewHandler.ControllerInfo.Endpoints.FirstOrDefault(x => x.MethodInfo.ToString() == method);

        if (endpoint is null)
        {
            _logger.LogDebug("Button press has been skipped because of missing action method");

            await next(context);
            return;
        }

        var viewActionContext = new ViewActionContext(buttonContext);

        var viewContext = new InternalViewContext(endpoint, parameters, viewState, viewActionContext);

        var keyFactoryAcceptor = context.ServiceProvider.GetRequiredService<IStateKeyFactoryAcceptor>();
        keyFactoryAcceptor.SetKeyFactory(new ViewStateKeyFactory(viewContext));

        await viewHandler.Handler.HandleAsync(viewContext, context.ServiceProvider, context.CancellationToken);
    }
}
