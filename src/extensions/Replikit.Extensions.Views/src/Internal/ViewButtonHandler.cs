using Replikit.Abstractions.Messages.Events;
using Replikit.Core.Handlers;
using Replikit.Extensions.Views.Messages;
using System.Text.Json;
using Kantaiko.Routing;
using Microsoft.Extensions.Logging;
using Replikit.Abstractions.Messages.Models;
using Replikit.Extensions.Common.Views;

namespace Replikit.Extensions.Views.Internal;

internal class ViewButtonHandler : AdapterEventHandler<ButtonPressedEvent>
{
    private readonly ViewRequestHandlerAccessor _requestHandlerAccessor;
    private readonly ILogger<ViewButtonHandler> _logger;
    private readonly IViewStorage _viewStorage;
    private readonly ViewRequestContextAccessor _viewRequestContextAccessor;

    public ViewButtonHandler(ViewRequestHandlerAccessor requestHandlerAccessor, ILogger<ViewButtonHandler> logger,
        IViewStorage viewStorage, ViewRequestContextAccessor viewRequestContextAccessor)
    {
        _requestHandlerAccessor = requestHandlerAccessor;
        _logger = logger;
        _viewStorage = viewStorage;
        _viewRequestContextAccessor = viewRequestContextAccessor;
    }

    public override async Task<Unit> HandleAsync(IEventContext<ButtonPressedEvent> context, NextAction next)
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
        var requestContext = new ViewContext(request, ServiceProvider, CancellationToken);

        _viewRequestContextAccessor.Context = requestContext;

        await _requestHandlerAccessor.RequestHandler.HandleAsync(requestContext, context.ServiceProvider,
            context.CancellationToken);

        return default;
    }
}
