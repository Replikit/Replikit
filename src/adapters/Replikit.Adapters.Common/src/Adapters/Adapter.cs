using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Adapters.Services;
using Replikit.Abstractions.Attachments.Services;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Events;
using Replikit.Abstractions.Management.Services;
using Replikit.Abstractions.Messages.Services;
using Replikit.Abstractions.Repositories.Services;
using Replikit.Adapters.Common.Adapters.Internal;
using Replikit.Adapters.Common.Services.Internal;

namespace Replikit.Adapters.Common.Adapters;

public abstract class Adapter : IAdapter
{
    private readonly AdapterServiceProvider _serviceProvider = new();

    public AdapterIdentifier Id { get; }

    protected Adapter(AdapterIdentifier id, AdapterFactoryContext context)
    {
        Id = id;

        AttachmentCache = context.AttachmentCache ?? DefaultAttachmentCache.Instance;
        EventDispatcher = context.EventDispatcher ?? DefaultEventDispatcher.Instance;
    }

    public object? GetService(Type serviceType)
    {
        return _serviceProvider.GetService(serviceType);
    }

    protected void SetService<TService>(TService instance)
    {
        _serviceProvider.SetService(instance);
    }

    public AdapterInfo Info
    {
        get => this.GetRequiredService<AdapterInfo>();
        internal set => SetService(value);
    }

    public IEventSource EventSource
    {
        get => this.GetRequiredService<IEventSource>();
        protected init => SetService(value);
    }

    public IAdapterRepository Repository
    {
        get => this.GetRequiredService<IAdapterRepository>();
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
        protected init => SetService<IMessageService>(new CommonMessageService(Id, AttachmentCache, value));
    }

    public IMemberService MemberService
    {
        get => this.GetRequiredService<IMemberService>();
        protected init => SetService<IMemberService>(new CommonMemberService(Id, value));
    }

    public IChannelService ChannelService
    {
        get => this.GetRequiredService<IChannelService>();
        protected init => SetService<IChannelService>(new CommonChannelService(Id, value));
    }

    protected IAdapterEventDispatcher EventDispatcher
    {
        get => this.GetRequiredService<IAdapterEventDispatcher>();
        private init => SetService(value);
    }

    protected IAttachmentCache AttachmentCache
    {
        get => this.GetRequiredService<IAttachmentCache>();
        private init => SetService(value);
    }
}
