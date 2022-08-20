using Replikit.Abstractions.Common.CustomData;
using Replikit.Abstractions.Messages.Models.TextTokens;
using Replikit.Abstractions.Messages.Services;
using Replikit.Adapters.Telegram.Internal;
using Telegram.Bot.Types;
using Message = Replikit.Abstractions.Messages.Models.Message;
using TelegramMessage = Telegram.Bot.Types.Message;

namespace Replikit.Adapters.Telegram.Services;

internal class TelegramTextTokenizer : ITextTokenizer
{
    public IReadOnlyList<TextToken> Tokenize(Message message)
    {
        var original = message.GetCustomData<TelegramMessage>();

        if (original.Text is null) return Array.Empty<TextToken>();

        var messageTokenizer = new MessageTokenizer(original.Text, original.Entities ?? Array.Empty<MessageEntity>());
        return messageTokenizer.Tokenize();
    }
}
