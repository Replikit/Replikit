using Replikit.Abstractions.Accounts.Models;
using Replikit.Abstractions.Events;

namespace Replikit.Abstractions.Accounts.Events;

/// <summary>
/// Represents an event that occured with an account.
/// </summary>
public interface IAccountEvent : IBotEvent
{
    /// <summary>
    /// The account that caused the event.
    /// </summary>
    AccountInfo Account { get; }
}
