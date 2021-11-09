namespace Replikit.Abstractions.Messages.Models.InlineButtons;

public record LinkInlineButton(string Text, Uri Url) : IInlineButton;
