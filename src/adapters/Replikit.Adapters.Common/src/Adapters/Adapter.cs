using Replikit.Abstractions.Accounts.Services;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Adapters.Factory;
using Replikit.Abstractions.Attachments.Services;
using Replikit.Abstractions.Channels.Services;
using Replikit.Abstractions.Common.Exceptions;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Events;
using Replikit.Abstractions.Members.Services;
using Replikit.Abstractions.Messages.Services;
using Replikit.Adapters.Common.Adapters.Internal;
using Replikit.Adapters.Common.Resources;
using Replikit.Adapters.Common.Services.Internal;

namespace Replikit.Adapters.Common.Adapters;

public abstract class Adapter : IAdapter
{
    private readonly AdapterServiceProvider _serviceProvider = new();

    protected Adapter(AdapterInfo adapterInfo, PlatformInfo platformInfo, AdapterFactoryContext context)
    {
        AdapterInfo = adapterInfo;
        PlatformInfo = platformInfo;

        AttachmentCache = context.AttachmentCache ?? DefaultAttachmentCache.Instance;
        EventDispatcher = context.EventDispatcher ?? DefaultEventDispatcher.Instance;
    }

    public IServiceProvider AdapterServices => _serviceProvider;

    protected void SetService<TService>(TService service)
    {
        _serviceProvider.SetService(service);
    }

    public AdapterInfo AdapterInfo { get; }
    public PlatformInfo PlatformInfo { get; }
    public AdapterBotInfo BotInfo { get; private set; } = default!;

    protected abstract Task<AdapterBotInfo> InitializeAsync(CancellationToken cancellationToken);

    internal async Task InitializeCoreAsync(CancellationToken cancellationToken)
    {
        BotInfo = await InitializeAsync(cancellationToken);
    }

    protected IAdapterEventDispatcher EventDispatcher { get; }
    protected IAttachmentCache AttachmentCache { get; }

    public IAdapterEventSource AdapterEventSource
    {
        get => this.GetRequiredService<IAdapterEventSource>();
        protected init => SetService(value);
    }

    public ITextFormatter TextFormatter
    {
        get => this.GetRequiredService<ITextFormatter>();
        protected init => SetService(value);
    }

    public ITextTokenizer TextTokenizer
    {
        get => this.GetRequiredService<ITextTokenizer>();
        protected init => SetService(value);
    }

    public IMessageService MessageService
    {
        get => this.GetRequiredService<IMessageService>();
        protected init => SetService<IMessageService>(new CommonMessageService(this, value, AttachmentCache));
    }

    public IAccountService AccountService
    {
        get => this.GetRequiredService<IAccountService>();
        protected init => SetService<IAccountService>(new CommonAccountService(this, value));
    }

    public IMemberService MemberService
    {
        get => this.GetRequiredService<IMemberService>();
        protected init => SetService<IMemberService>(new CommonMemberService(this, value));
    }

    public IAttachmentService AttachmentService
    {
        get => this.GetRequiredService<IAttachmentService>();
        protected init => SetService<IAttachmentService>(new CommonAttachmentService(value));
    }

    public IChannelService ChannelService
    {
        get => this.GetRequiredService<IChannelService>();
        protected init => SetService<IChannelService>(new CommonChannelService(this, value));
    }
}
