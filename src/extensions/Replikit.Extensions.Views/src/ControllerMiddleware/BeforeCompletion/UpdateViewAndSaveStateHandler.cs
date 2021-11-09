using System.Data;
using System.Diagnostics;
using Kantaiko.Controllers.Middleware;
using Microsoft.Extensions.Logging;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Messages.Models;
using Replikit.Extensions.Common.Models;
using Replikit.Extensions.Common.Utils;
using Replikit.Extensions.Common.Views;
using Replikit.Extensions.Views.Internal;
using Replikit.Extensions.Views.Messages;

namespace Replikit.Extensions.Views.ControllerMiddleware.BeforeCompletion;

internal class UpdateViewAndSaveStateHandler : EndpointMiddleware<ViewContext>
{
    private readonly ILogger<UpdateViewAndSaveStateHandler> _logger;
    private readonly IAdapterCollection _adapterCollection;
    private readonly IViewStorage _viewStorage;
    private readonly ViewExternalActivationDeterminant _viewExternalActivationDeterminant;

    public UpdateViewAndSaveStateHandler(ILogger<UpdateViewAndSaveStateHandler> logger,
        IAdapterCollection adapterCollection, IViewStorage viewStorage,
        ViewExternalActivationDeterminant viewExternalActivationDeterminant)
    {
        _logger = logger;
        _adapterCollection = adapterCollection;
        _viewStorage = viewStorage;
        _viewExternalActivationDeterminant = viewExternalActivationDeterminant;
    }

    public override EndpointMiddlewareStage Stage => EndpointMiddlewareStage.BeforeCompletion;

    public override async Task HandleAsync(EndpointMiddlewareContext<ViewContext> context,
        CancellationToken cancellationToken)
    {
        var controller = context.ExecutionContext.ControllerInstance;
        Debug.Assert(controller is not null);

        if (controller is not View view)
        {
            throw new InvalidOperationException(
                $"Controller of type \"{controller.GetType().Name}\" is not a View");
        }

        var viewRequest = context.RequestContext.Request;
        var viewInstance = viewRequest.ViewInstance;

        var controllerInfo = context.ExecutionContext.Endpoint.Controller;

        var statefulView = view as IStatefulView;

        async Task SaveChanges()
        {
            if (viewRequest.AutoSave)
            {
                await _viewStorage.SaveChangesAsync(cancellationToken);
            }
        }

        var shouldKeepView = true;

        void TryDeleteView()
        {
            if (_viewExternalActivationDeterminant.IsExternalActivationAllowed(controllerInfo))
            {
                _logger.LogDebug("Keeping view state since it's external activation is allowed");
                return;
            }

            shouldKeepView = false;
            _logger.LogDebug("View state deleted since view became inaccessible");
        }

        ViewInstanceAction CreateViewEntryAction(ViewAction action)
        {
            var (method, parameters) = MethodExpressionTransformer.Transform(action.Action);

            var hasAction = controllerInfo.Endpoints.Any(x =>
                x.MethodInfo.ToString() == method.ToString() &&
                x.Properties.ContainsKey(ViewEndpointProperties.IsAction));

            if (!hasAction)
            {
                throw new InvalidExpressionException(
                    $"Method {method.Name} is not defined in the view " +
                    "or is not marked with [Action] attribute");
            }

            return new ViewInstanceAction(method.ToString()!, parameters);
        }

        async Task<(OutMessage, IReadOnlyList<ViewInstanceAction>)> RenderView()
        {
            var viewResult = await view.RenderAsync(cancellationToken);

            if (viewResult.OutMessage is not null)
            {
                TryDeleteView();
                return (viewResult.OutMessage, Array.Empty<ViewInstanceAction>());
            }

            var (message, actions) = viewResult.ViewMessageBuilder!.BuildWithActions();

            if (actions.Count == 0)
            {
                TryDeleteView();
            }

            return (message, actions.Select(CreateViewEntryAction).ToArray());
        }

        var (viewMessage, viewActions) = await RenderView();

        async Task<GlobalMessageIdentifier> UpsertMessage()
        {
            if (viewInstance is null)
            {
                if (viewRequest.MessageCollection is null)
                {
                    throw new InvalidOperationException("No message collection nor view instance provided");
                }

                var message = await viewRequest.MessageCollection.SendAsync(viewMessage,
                    cancellationToken: cancellationToken);

                _logger.LogDebug("View message created");

                return message.Id;
            }

            var messageId = viewInstance.MessageId;
            if (!view.UpdateRequested) return messageId;

            var channelId = messageId.ChannelId;

            if (channelId is null)
            {
                throw new InvalidOperationException("View message id is null");
            }

            var adapter = _adapterCollection.ResolveRequired(channelId.AdapterId);

            await adapter.MessageService.EditAsync(messageId.ChannelId, messageId, viewMessage,
                cancellationToken: cancellationToken);

            _logger.LogDebug("View message updated");

            return messageId;
        }

        var messageId = await UpsertMessage();

        if (shouldKeepView)
        {
            var entry = new ViewInstance(messageId,
                viewRequest.Type,
                new DynamicValue(statefulView?.StateValue),
                viewActions);

            _viewStorage.Set(messageId, entry);
            _logger.LogDebug("View state written");
        }
        else if (viewInstance is not null)
        {
            Debug.Assert(messageId == viewInstance.MessageId);

            _viewStorage.Delete(messageId);
            _logger.LogDebug("View state deleted");
        }

        await SaveChanges();
    }
}
