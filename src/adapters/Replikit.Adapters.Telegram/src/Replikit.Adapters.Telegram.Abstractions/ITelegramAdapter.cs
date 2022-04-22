using Replikit.Abstractions.Adapters;
using Telegram.Bot;

namespace Replikit.Adapters.Telegram.Abstractions;

public interface ITelegramAdapter : IAdapter
{
    ITelegramBotClient Backend { get; }
}
