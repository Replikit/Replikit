using Microsoft.Extensions.Caching.Memory;
using Replikit.Abstractions.Accounts.Models;
using Replikit.Abstractions.Accounts.Services;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Common.Utilities;
using Replikit.Adapters.Common.Extensions;

namespace Replikit.Adapters.Common.Services.Internal;

internal class CommonAccountService : AdapterService, IAccountService
{
    private readonly IMemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
    private readonly TimeSpan _cacheLifetime = TimeSpan.FromMinutes(10);

    private readonly IAccountService _accountService;

    public CommonAccountService(IAdapter adapter, IAccountService accountService) : base(adapter)
    {
        _accountService = accountService;
    }

    public AccountServiceFeatures Features => _accountService.Features;

    public Task<AccountInfo?> GetAsync(Identifier accountId, CancellationToken cancellationToken = default)
    {
        Check.NotDefault(accountId);

        return _cache.GetOrCreateAsync(accountId, _accountService.GetAsync, _cacheLifetime, cancellationToken);
    }

    public Task<PhotoAttachment?> GetAvatarAsync(Identifier accountId, CancellationToken cancellationToken = default)
    {
        Check.NotDefault(accountId);

        // TODO caching
        return _accountService.GetAvatarAsync(accountId, cancellationToken);
    }
}
