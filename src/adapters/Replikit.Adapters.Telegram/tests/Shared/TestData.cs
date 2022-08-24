using Telegram.Bot.Types;

namespace Replikit.Adapters.Telegram.Tests.Shared;

internal static class TestData
{
    public const string TestToken = "test:token:42";

    public static readonly User TestBotUser = new()
    {
        Id = 1,
        IsBot = true,
        FirstName = "Test Bot",
    };
}
