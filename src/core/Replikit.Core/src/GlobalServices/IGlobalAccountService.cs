using Replikit.Abstractions.Accounts.Models;
using Replikit.Abstractions.Accounts.Services;
using Replikit.Abstractions.Common.Models;

namespace Replikit.Core.GlobalServices;

public interface IGlobalAccountService : IGlobalHasFeatures<AccountServiceFeatures>
{
    Task<AccountInfo?> GetAsync(GlobalIdentifier accountId, CancellationToken cancellationToken = default);
}
