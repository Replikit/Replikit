using Replikit.Abstractions.Messages.Events;
using Replikit.Core.Handlers;
using Replikit.Extensions.Views.Messages;
using System.Text.Json;
using Kantaiko.Routing;
using Kantaiko.Routing.Events;
using Microsoft.Extensions.Logging;
using Replikit.Extensions.Common.Views;

namespace Replikit.Extensions.Views.Internal;

internal class ViewButtonHandler : AdapterEventHandler<ButtonPressedEvent>
{
    private readonly ViewHandlerAccessor _handlerAccessor;
    private readonly ILogger<ViewButtonHandler> _logger;
    private readonly IViewStorage _viewStorage;

    public ViewButtonHandler(ViewHandlerAccessor handlerAccessor, ILogger<ViewButtonHandler> logger,
        IViewStorage viewStorage)
    {
        _handlerAccessor = handlerAccessor;
        _logger = logger;
        _viewStorage = viewStorage;
    }

    protected override async Task<Unit> HandleAsync(IEventContext<ButtonPressedEvent> context, NextAction next)
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

        var viewEntry = await _viewStorage.GetAsync(context.Event.Message.Id, context.CancellationToken);

        if (viewEntry is null)
        {
            _logger.LogDebug("Button press has been skipped because view was not found");
            return await next();
        }

        if (viewEntry.Actions is not { Count: > 0 })
        {
            _logger.LogDebug("Button press has been skipped because view contains no actions");
            return await next();
        }

        if (payload.ActionIndex < 0 || payload.ActionIndex >= viewEntry.Actions.Count)
        {
            _logger.LogDebug("Button press has been skipped because of invalid index");
            return await next();
        }

        var action = viewEntry.Actions[payload.ActionIndex];

        var request = new ViewRequest(viewEntry.Type, action.Method, action.Parameters, viewEntry, Event);
        var viewContext = new ViewContext(request, ServiceProvider, cancellationToken: CancellationToken);

        await _handlerAccessor.Handler.Handle(viewContext);

        return default;
    }
}
