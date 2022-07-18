using Replikit.Abstractions.Messages.Events;
using Replikit.Core.Handlers;
using Replikit.Extensions.Views.Messages;
using System.Text.Json;
using Kantaiko.Routing;
using Kantaiko.Routing.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Replikit.Core.Handlers.Context;
using Replikit.Extensions.State;
using Replikit.Extensions.Views.Models;

namespace Replikit.Extensions.Views.Internal;

internal class ViewButtonHandler : AdapterEventHandler<ButtonPressedEvent>
{
    private readonly ViewHandlerAccessor _handlerAccessor;
    private readonly ILogger<ViewButtonHandler> _logger;
    private readonly IStateManager _stateManager;

    public ViewButtonHandler(ViewHandlerAccessor handlerAccessor,
        ILogger<ViewButtonHandler> logger,
        IStateManager stateManager)
    {
        _handlerAccessor = handlerAccessor;
        _logger = logger;
        _stateManager = stateManager;
    }

    protected override async Task<Unit> HandleAsync(IAdapterEventContext<ButtonPressedEvent> context, NextAction next)
    {
        if (context.Event.Message is null)
        {
            _logger.LogDebug("Button press has been skipped because of inaccessible message");
            return await next();
        }

        ViewActionPayload? payload;

        try
        {
            if (string.IsNullOrEmpty(context.Event.Data)) throw new JsonException();

            payload = JsonSerializer.Deserialize<ViewActionPayload>(context.Event.Data);
            if (payload is null) throw new JsonException();
        }
        catch (JsonException)
        {
            _logger.LogDebug("Button press has been skipped because of invalid payload");
            return await next();
        }

        var viewState = await _stateManager.GetViewStateAsync<ViewState>(
            context.Event.Message.Id, context.CancellationToken);

        var viewInstance = viewState.Value.ViewInstance;

        if (viewInstance is null)
        {
            _logger.LogDebug("Button press has been skipped because view was not found");
            return await next();
        }

        if (viewInstance.Actions is not { Count: > 0 })
        {
            _logger.LogDebug("Button press has been skipped because view contains no actions");
            return await next();
        }

        if (payload.ActionIndex < 0 || payload.ActionIndex >= viewInstance.Actions.Count)
        {
            _logger.LogDebug("Button press has been skipped because of invalid index");
            return await next();
        }

        var (method, parameters) = viewInstance.Actions[payload.ActionIndex];

        await using var scope = ServiceProvider.CreateAsyncScope();

        var request = new ViewRequest(viewInstance.Type, method, parameters, viewState, Context);
        var viewContext = new ViewContext(request, scope.ServiceProvider, CancellationToken);

        await _handlerAccessor.Handler.HandleAsync(viewContext, scope.ServiceProvider, CancellationToken);

        return default;
    }
}
