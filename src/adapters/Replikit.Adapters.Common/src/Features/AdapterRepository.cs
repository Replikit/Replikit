using Microsoft.Extensions.Caching.Memory;
using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Repositories.Features;
using Replikit.Abstractions.Repositories.Models;
using Replikit.Adapters.Common.Extensions;

namespace Replikit.Adapters.Common.Features;

public abstract class AdapterRepository : IAdapterRepository
{
    private readonly IMemoryCache _accountCache = new MemoryCache(new MemoryCacheOptions());
    private readonly IMemoryCache _channelCache = new MemoryCache(new MemoryCacheOptions());

    private readonly TimeSpan _accountCacheLifetime = TimeSpan.FromMinutes(10);
    private readonly TimeSpan _channelCacheLifetime = TimeSpan.FromMinutes(10);

    public abstract AdapterRepositoryFeatures Features { get; }

    protected abstract Task<ChannelInfo?> FetchChannelInfo(Identifier identifier, CancellationToken cancellationToken);
    protected abstract Task<AccountInfo?> FetchAccountInfo(Identifier identifier, CancellationToken cancellationToken);

    public Task<ChannelInfo?> GetChannelInfoAsync(Identifier identifier, CancellationToken cancellationToken = default)
    {
        return _channelCache.GetOrCreateAsync(identifier,
            FetchChannelInfo, _accountCacheLifetime, cancellationToken);
    }

    public Task<AccountInfo?> GetAccountInfoAsync(Identifier identifier, CancellationToken cancellationToken = default)
    {
        return _accountCache.GetOrCreateAsync(identifier,
            FetchAccountInfo, _channelCacheLifetime, cancellationToken);
    }

    public virtual Task<Attachment> ResolveAttachmentUrlAsync(Attachment attachment,
        CancellationToken cancellationToken = default) => Task.FromResult(attachment);

    public ChannelInfo UpdateChannelInfo(ChannelInfo channelInfo)
    {
        var existing = _channelCache.Get<ChannelInfo>(channelInfo.Id);

        if (existing is null)
        {
            _channelCache.Set(channelInfo.Id, channelInfo);
            return channelInfo;
        }

        var newRecord = new ChannelInfo(channelInfo.Id,
            channelInfo.Type,
            channelInfo.Title ?? existing.Title,
            channelInfo.ParentId ?? existing.ParentId);

        _channelCache.Set(newRecord.Id, newRecord);

        return newRecord;
    }

    public AccountInfo UpdateAccountInfo(AccountInfo accountInfo)
    {
        var existing = _accountCache.Get<AccountInfo>(accountInfo.Id);

        if (existing is null)
        {
            _accountCache.Set(accountInfo.Id, accountInfo);
            return accountInfo;
        }

        var newRecord = new AccountInfo(
            accountInfo.Id,
            accountInfo.Username ?? existing.Username,
            accountInfo.FirstName ?? existing.FirstName,
            accountInfo.LastName ?? existing.LastName,
            accountInfo.Avatar ?? existing.Avatar,
            accountInfo.CultureInfo ?? existing.CultureInfo);

        _accountCache.Set(newRecord.Id, newRecord);

        return newRecord;
    }
}
