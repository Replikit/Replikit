using Replikit.Adapters.Common.Exceptions;

namespace Replikit.Adapters.Telegram.Exceptions;

public class TelegramAdapterException : ReplikitAdapterException
{
    public TelegramAdapterException(string message) : base(message) { }
}
