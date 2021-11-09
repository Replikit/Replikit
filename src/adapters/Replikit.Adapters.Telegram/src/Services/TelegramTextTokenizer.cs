using Replikit.Abstractions.Messages.Features;
using Replikit.Abstractions.Messages.Models.Tokens;
using Replikit.Adapters.Telegram.Internal;
using Message = Replikit.Abstractions.Messages.Models.Message;
using TelegramMessage = Telegram.Bot.Types.Message;

namespace Replikit.Adapters.Telegram.Services;

internal class TelegramTextTokenizer : ITextTokenizer
{
    public IReadOnlyList<TextToken> TokenizeMessage(Message message)
    {
        var original = message.GetOriginal<TelegramMessage>();
        var messageTokenizer = new MessageTokenizer(original.Text, original.Entities);
        return messageTokenizer.Tokenize();
    }
}
