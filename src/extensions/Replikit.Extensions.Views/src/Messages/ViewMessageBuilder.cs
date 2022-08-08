using System.Linq.Expressions;
using System.Text.Json;
using Replikit.Abstractions.Messages.Builder;
using Replikit.Abstractions.Messages.Events;
using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Messages.Models.InlineButtons;
using Replikit.Core.Handlers.Context;
using Replikit.Extensions.Views.Internal;

namespace Replikit.Extensions.Views.Messages;

public class ViewMessageBuilder : MessageBuilder<ViewMessageBuilder>
{
    private readonly List<ViewMessageAction> _actions = new();

    public ViewMessageBuilder AddActionRow(params ViewMessageAction[] actions)
    {
        InlineButtonBuilder.AddButtonRow(actions.Select(CreateButton));
        _actions.AddRange(actions);

        return this;
    }

    public ViewMessageBuilder AddActionRow()
    {
        InlineButtonBuilder.AddButtonRow();

        return this;
    }

    public delegate void ViewActionFactory(IAdapterEventContext<ButtonPressedEvent> context);

    public delegate Task ViewActionAsyncFactory(IAdapterEventContext<ButtonPressedEvent> context);

    public ViewMessageBuilder AddAction(int row, string text, Expression<Action> action)
    {
        return AddAction(row, new ViewMessageAction(text, action));
    }

    public ViewMessageBuilder AddAction(int row, string text, Expression<Func<Task>> action)
    {
        return AddAction(row, new ViewMessageAction(text, action));
    }

    public ViewMessageBuilder AddAction(int row, string text, Expression<ViewActionFactory> action)
    {
        return AddAction(row, new ViewMessageAction(text, action));
    }

    public ViewMessageBuilder AddAction(int row, string text, Expression<ViewActionAsyncFactory> action)
    {
        return AddAction(row, new ViewMessageAction(text, action));
    }

    public ViewMessageBuilder AddAction(string text, Expression<Action> action)
    {
        return AddAction(new ViewMessageAction(text, action));
    }

    public ViewMessageBuilder AddAction(string text, Expression<Func<Task>> action)
    {
        return AddAction(new ViewMessageAction(text, action));
    }

    public ViewMessageBuilder AddAction(string text, Expression<ViewActionFactory> action)
    {
        return AddAction(new ViewMessageAction(text, action));
    }

    public ViewMessageBuilder AddAction(string text, Expression<ViewActionAsyncFactory> action)
    {
        return AddAction(new ViewMessageAction(text, action));
    }

    private ViewMessageBuilder AddAction(ViewMessageAction messageAction)
    {
        _actions.Add(messageAction);
        InlineButtonBuilder.AddButton(CreateButton(messageAction));

        return this;
    }

    private ViewMessageBuilder AddAction(int row, ViewMessageAction messageAction)
    {
        _actions.Add(messageAction);
        InlineButtonBuilder.AddButton(row, CreateButton(messageAction));

        return this;
    }

    private IInlineButton CreateButton(ViewMessageAction messageAction, int index = 0)
    {
        var payload = JsonSerializer.Serialize(new ViewActionPayload(_actions.Count - 1 + index));

        return new CallbackInlineButton(messageAction.Text, payload);
    }

    internal (OutMessage, IReadOnlyList<ViewMessageAction>) BuildWithActions()
    {
        return (Build(), _actions);
    }
}
