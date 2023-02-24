using Replikit.Abstractions.Accounts.Models;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Common.Utilities;

namespace Replikit.Abstractions.Adapters;

/// <summary>
/// Represents an information about the bot authenticated by the adapter.
/// </summary>
/// <param name="Id">The identifier of the bot.</param>
/// <param name="Account">The account of the bot.</param>
public record AdapterBotInfo(BotIdentifier Id, AccountInfo Account)
{
    /// <summary>
    /// The identifier of the bot.
    /// </summary>
    public BotIdentifier Id { get; } = Check.NotDefault(Id);

    /// <summary>
    /// The information of the bot's account.
    /// </summary>
    public AccountInfo Account { get; } = Check.NotNull(Account);
}
