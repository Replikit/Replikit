namespace Replikit.Abstractions.Messages.Models.InlineButtons;

public record CallbackInlineButton(string Text, string Data) : IInlineButton;
