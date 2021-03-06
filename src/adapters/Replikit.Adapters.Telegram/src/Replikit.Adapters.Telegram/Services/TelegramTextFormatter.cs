using System.Web;
using Replikit.Abstractions.Messages.Models.Tokens;
using Replikit.Adapters.Common.Text.Formatting;

namespace Replikit.Adapters.Telegram.Services;

internal class TelegramTextFormatter : TextFormatter
{
    public TelegramTextFormatter()
    {
        AddModifiersFormatter(TextTokenModifiers.Bold, "<b>", "</b>");
        AddModifiersFormatter(TextTokenModifiers.Code, "<pre>", "</pre>");
        AddModifiersFormatter(TextTokenModifiers.Italic, "<i>", "</i>");
        AddModifiersFormatter(TextTokenModifiers.Strikethrough, "<s>", "</s>");
        AddModifiersFormatter(TextTokenModifiers.Underline, "<u>", "</u>");
        AddModifiersFormatter(TextTokenModifiers.InlineCode, "<code>", "</code>");

        AddVisitor<TextToken>(token => HttpUtility.HtmlEncode(token.Text));
        AddVisitor<LinkTextToken>(token => $"<a href=\"{token.Url}\">{HttpUtility.HtmlEncode(token.Text)}</a>");

        AddVisitor<MentionTextToken>(token =>
            $"<a href=\"tg://user?id=${token.AccountId}\">${HttpUtility.HtmlEncode(token.Text)}</a>");
    }
}
