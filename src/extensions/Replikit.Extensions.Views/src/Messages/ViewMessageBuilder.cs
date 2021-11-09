using System.Linq.Expressions;
using System.Text.Json;
using Replikit.Abstractions.Messages.Builder;
using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Messages.Models.InlineButtons;

namespace Replikit.Extensions.Views.Messages;

public class ViewMessageBuilder : MessageBuilder<ViewMessageBuilder>
{
    private readonly List<ViewAction> _actions = new();

    public ViewMessageBuilder AddActionRow(params ViewAction[] actions)
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

    public ViewMessageBuilder AddAction(ViewAction action)
    {
        _actions.Add(action);
        InlineButtonBuilder.AddButton(CreateButton(action));

        return this;
    }

    public ViewMessageBuilder AddAction(int row, ViewAction action)
    {
        _actions.Add(action);
        InlineButtonBuilder.AddButton(row, CreateButton(action));

        return this;
    }

    public ViewMessageBuilder AddAction(int row, string text, Expression<Action> action)
    {
        return AddAction(row, new ViewAction(text, action));
    }

    public ViewMessageBuilder AddAction(int row, string text, Expression<Func<Task>> action)
    {
        return AddAction(row, new ViewAction(text, action));
    }

    public ViewMessageBuilder AddAction(string text, Expression<Action> action)
    {
        return AddAction(new ViewAction(text, action));
    }

    public ViewMessageBuilder AddAction(string text, Expression<Func<Task>> action)
    {
        return AddAction(new ViewAction(text, action));
    }

    private IInlineButton CreateButton(ViewAction action, int index = 0)
    {
        var payload = JsonSerializer.Serialize(new ViewActionPayload(_actions.Count - 1 + index));
        return new CallbackInlineButton(action.Text, payload);
    }

    internal (OutMessage, IReadOnlyList<ViewAction>) BuildWithActions()
    {
        return (Build(), _actions);
    }
}
