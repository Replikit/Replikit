using System.Data;
using System.Diagnostics;
using Kantaiko.Controllers.Execution;
using Kantaiko.Controllers.Execution.Handlers;
using Kantaiko.Controllers.Result;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;
using Replikit.Core.Common;
using Replikit.Core.GlobalServices;
using Replikit.Core.Utils;
using Replikit.Extensions.State;
using Replikit.Extensions.State.Implementation;
using Replikit.Extensions.Views.Internal;
using Replikit.Extensions.Views.Messages;
using Replikit.Extensions.Views.Models;

namespace Replikit.Extensions.Views.ExecutionHandlers;

internal class UpdateViewAndSaveStateHandler : ControllerExecutionHandler<ViewContext>
{
    private readonly ILogger<UpdateViewAndSaveStateHandler> _logger;
    private readonly IAdapterCollection _adapterCollection;
    private readonly ViewExternalActivationDeterminant _viewExternalActivationDeterminant;
    private readonly IGlobalMessageService _messageService;

    public UpdateViewAndSaveStateHandler(ILogger<UpdateViewAndSaveStateHandler> logger,
        IAdapterCollection adapterCollection,
        ViewExternalActivationDeterminant viewExternalActivationDeterminant,
        IGlobalMessageService messageService)
    {
        _logger = logger;
        _adapterCollection = adapterCollection;
        _viewExternalActivationDeterminant = viewExternalActivationDeterminant;
        _messageService = messageService;
    }

    protected override async Task<ControllerResult> HandleAsync(ControllerContext<ViewContext> context, NextAction next)
    {
        var controller = context.ControllerInstance!;

        if (controller is not View view)
        {
            throw new InvalidOperationException(
                $"Controller of type \"{controller.GetType().Name}\" is not a View");
        }

        var viewRequest = context.RequestContext.Request;
        var viewState = viewRequest.ViewState;
        var viewInstance = viewState?.Value.ViewInstance;

        var controllerInfo = context.Endpoint!.Controller!;

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

        ViewInstanceAction CreateViewInstanceAction(ViewMessageAction action)
        {
            var (method, parameters) = MethodExpressionTransformer.Transform(action.Action);

            var hasAction = controllerInfo.Endpoints.Any(x =>
                x.MethodInfo.ToString() == method.ToString() &&
                ViewActionEndpointProperties.Of(x)?.IsAction is true);

            if (!hasAction)
            {
                throw new InvalidExpressionException(
                    $"Method {method.Name} is not defined in the view " +
                    "or is not marked with [Action] attribute");
            }

            var dynamicParameters = parameters.Select(x => new DynamicValue(x)).ToArray();

            return new ViewInstanceAction(method.ToString()!, dynamicParameters);
        }

        async Task<(OutMessage, IReadOnlyList<ViewInstanceAction>)> RenderView()
        {
            var viewResult = await view.RenderAsync(context.CancellationToken);

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

            return (message, actions.Select(CreateViewInstanceAction).ToArray());
        }

        var (viewMessage, viewActions) = await RenderView();

        async Task<GlobalMessageIdentifier> UpsertMessage()
        {
            if (viewInstance is null)
            {
                if (viewRequest.ChannelId is null)
                {
                    throw new InvalidOperationException("No channel id nor view instance provided");
                }

                var message = await _messageService.SendAsync(viewRequest.ChannelId.Value, viewMessage,
                    cancellationToken: context.CancellationToken);

                _logger.LogDebug("View message created");

                return message.Id;
            }

            // ReSharper disable once AccessToModifiedClosure
            var stateKey = viewState!.Key;

            if (view.UpdateRequested || ViewActionEndpointProperties.Of(context.Endpoint) is { AutoUpdate: true })
            {
                if (stateKey.MessageId is null)
                {
                    throw new InvalidOperationException("View message id is null");
                }

                var adapter = _adapterCollection.ResolveRequired(stateKey.AdapterId!);

                await adapter.MessageService.EditAsync(stateKey.ChannelId!.Value, stateKey.MessageId!.Value,
                    viewMessage, cancellationToken: context.CancellationToken);

                _logger.LogDebug("View message updated");
            }

            return new GlobalMessageIdentifier(
                new GlobalIdentifier(stateKey.AdapterId!, stateKey.ChannelId!.Value),
                stateKey.MessageId!.Value.Identifiers);
        }

        var messageId = await UpsertMessage();

        context.RequestContext.MessageId = messageId;

        var stateLoader = context.ServiceProvider.GetRequiredService<IStateLoader>();
        await stateLoader.LoadAsync(context.CancellationToken);

        if (shouldKeepView)
        {
            var instance = new ViewInstance(viewRequest.Type, viewActions);

            if (viewState is null)
            {
                var stateManager = context.ServiceProvider.GetRequiredService<IStateManager>();

                viewState = await stateManager.GetViewStateAsync<ViewState>(messageId, context.CancellationToken);
            }

            viewState.Value.ViewInstance = instance;

            _logger.LogDebug("View state updated");
        }
        else if (viewInstance is not null)
        {
            Debug.Assert(messageId == viewRequest.ViewState!.Key.MessageId);

            viewRequest.ViewState!.Clear();
            _logger.LogDebug("View state cleared");
        }

        await stateLoader.SaveAsync(context.CancellationToken);

        return ControllerResult.Empty;
    }
}
