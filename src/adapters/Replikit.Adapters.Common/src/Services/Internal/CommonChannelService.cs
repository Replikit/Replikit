using Microsoft.Extensions.Caching.Memory;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Channels.Models;
using Replikit.Abstractions.Channels.Services;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Common.Utilities;
using Replikit.Adapters.Common.Extensions;

namespace Replikit.Adapters.Common.Services.Internal;

internal class CommonChannelService : AdapterService, IChannelService
{
    private readonly IMemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
    private readonly TimeSpan _cacheLifetime = TimeSpan.FromMinutes(10);

    private readonly IChannelService _channelService;

    public CommonChannelService(IAdapter adapter, IChannelService channelService) : base(adapter)
    {
        _channelService = channelService;
    }

    public ChannelServiceFeatures Features => _channelService.Features;

    public Task<ChannelInfo?> GetAsync(Identifier channelId, CancellationToken cancellationToken = default)
    {
        Check.NotDefault(channelId);

        return _cache.GetOrCreateAsync(channelId, _channelService.GetAsync, _cacheLifetime, cancellationToken);
    }

    public Task ChangeTitleAsync(Identifier channelId, string title, CancellationToken cancellationToken = default)
    {
        Check.NotDefault(channelId);
        Check.NotNull(title);

        return _channelService.ChangeTitleAsync(channelId, title, cancellationToken);
    }
}
