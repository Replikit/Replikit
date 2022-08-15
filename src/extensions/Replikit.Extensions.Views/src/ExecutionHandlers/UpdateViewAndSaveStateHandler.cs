using System.Text.Json;
using Kantaiko.Controllers.Execution;
using Kantaiko.Controllers.Execution.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Messages.Models.InlineButtons;
using Replikit.Core.GlobalServices;
using Replikit.Extensions.State.Implementation;
using Replikit.Extensions.Views.Actions;
using Replikit.Extensions.Views.Internal;
using Replikit.Extensions.Views.Models;

namespace Replikit.Extensions.Views.ExecutionHandlers;

internal class UpdateViewAndSaveStateHandler : IControllerExecutionHandler<InternalViewContext>
{
    private readonly ILogger<UpdateViewAndSaveStateHandler> _logger;
    private readonly IGlobalMessageService _messageService;

    public UpdateViewAndSaveStateHandler(ILogger<UpdateViewAndSaveStateHandler> logger,
        IGlobalMessageService messageService)
    {
        _logger = logger;
        _messageService = messageService;
    }

    public async Task HandleAsync(ControllerExecutionContext<InternalViewContext> context)
    {
        var controller = context.ControllerInstance!;

        if (controller is not View view)
        {
            throw new InvalidOperationException(
                $"Controller of type \"{controller.GetType().Name}\" is not a View");
        }

        var requestContext = context.RequestContext;

        if (requestContext.ViewActionContext?.UpdateRequested is false)
        {
            return;
        }

        var viewState = requestContext.ViewState;
        var viewInstance = viewState.Value.ViewInstance;
        var controllerInfo = requestContext.ViewController;

        var viewMessage = await view.RenderAsync(context.CancellationToken);
        var actions = new List<ViewInstanceAction>();

        foreach (var actionRow in viewMessage.Actions)
        {
            var buttonRow = new List<IInlineButton>();

            foreach (var viewAction in actionRow)
            {
                var (method, parameters) = MethodExpressionHelper.ExtractInfo(viewAction.Action);

                var instanceAction = new ViewInstanceAction(method.ToString()!, parameters);

                buttonRow.Add(CreateActionButton(viewAction, actions.Count));
                actions.Add(instanceAction);
            }

            viewMessage.InlineButtons.Add(buttonRow);
        }

        async Task<GlobalMessageIdentifier> UpsertMessage()
        {
            if (viewInstance is null)
            {
                if (requestContext.ChannelId is null)
                {
                    throw new InvalidOperationException("No channel id nor view instance provided");
                }

                var message = await _messageService.SendAsync(
                    requestContext.ChannelId.Value,
                    viewMessage,
                    cancellationToken: context.CancellationToken
                );

                _logger.LogDebug("View message created");

                return message.Id;
            }

            // ReSharper disable once AccessToModifiedClosure
            var stateKey = viewState.Key;

            if (stateKey?.MessagePartId is null)
            {
                throw new InvalidOperationException("View message id is null");
            }

            var channelId = new GlobalIdentifier(stateKey.AdapterId!, stateKey.ChannelId!.Value);

            await _messageService.EditAsync(
                channelId,
                stateKey.MessagePartId!.Value,
                viewMessage,
                cancellationToken: context.CancellationToken
            );

            _logger.LogDebug("View message updated");

            return new GlobalMessageIdentifier(
                new GlobalIdentifier(stateKey.AdapterId!, stateKey.ChannelId!.Value),
                stateKey.MessagePartId!.Value
            );
        }

        requestContext.ViewMessageId = await UpsertMessage();

        var stateLoader = context.ServiceProvider.GetRequiredService<IStateLoader>();

        if (viewState.Key is null)
        {
            await stateLoader.LoadAsync(context.CancellationToken);
        }

        viewState.Value.ViewInstance = new ViewInstance(controllerInfo.Type.FullName!, actions);

        await stateLoader.SaveAsync(context.CancellationToken);
    }

    private static CallbackInlineButton CreateActionButton(ViewAction viewAction, int index)
    {
        var data = JsonSerializer.Serialize(new ViewActionPayload(index));

        return new CallbackInlineButton(viewAction.Text, data);
    }
}
