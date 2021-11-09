using Replikit.Abstractions.Events;
using Replikit.Abstractions.Repositories.Models;

namespace Replikit.Abstractions.Repositories.Events;

public interface IAccountEvent : IEvent
{
    AccountInfo Account { get; }
}
