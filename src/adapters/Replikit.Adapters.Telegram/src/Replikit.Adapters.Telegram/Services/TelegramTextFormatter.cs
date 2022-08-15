using System.Web;
using Replikit.Abstractions.Messages.Models.TextTokens;
using Replikit.Adapters.Common.Text.Formatting;

namespace Replikit.Adapters.Telegram.Services;

internal class TelegramTextFormatter : TextFormatter
{
    public TelegramTextFormatter()
    {
        AddModifiersFormatter(TextTokenModifiers.Bold, "<b>", "</b>");
        AddModifiersFormatter(TextTokenModifiers.Code, "<pre>", "</pre>");
        AddModifiersFormatter(TextTokenModifiers.Italic, "<i>", "</i>");
        AddModifiersFormatter(TextTokenModifiers.Strike, "<s>", "</s>");
        AddModifiersFormatter(TextTokenModifiers.Underline, "<u>", "</u>");
        AddModifiersFormatter(TextTokenModifiers.InlineCode, "<code>", "</code>");

        AddVisitor<TextToken>(token => HttpUtility.HtmlEncode(token.Text));
        AddVisitor<LinkTextToken>(token => $"<a href=\"{token.Url}\">{HttpUtility.HtmlEncode(token.Text)}</a>");

        AddVisitor<MentionTextToken>(token => token switch
        {
            { AccountId: not null } =>
                $"<a href=\"tg://user?id={token.AccountId}\">{HttpUtility.HtmlEncode(token.Text)}</a>",
            { Username: not null } => "@" + HttpUtility.HtmlEncode(token.Username),
            _ => throw new InvalidOperationException("MentionTextToken must have either AccountId or Username")
        });
    }
}
