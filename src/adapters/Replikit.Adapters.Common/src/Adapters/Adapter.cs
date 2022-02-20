using System.Runtime.CompilerServices;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Attachments.Features;
using Replikit.Abstractions.Common.Exceptions;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Events;
using Replikit.Abstractions.Management.Features;
using Replikit.Abstractions.Messages.Features;
using Replikit.Abstractions.Repositories.Features;
using Replikit.Adapters.Common.Adapters.Internal;
using Replikit.Adapters.Common.Features.Internal;

namespace Replikit.Adapters.Common.Adapters;

public abstract class Adapter : IAdapter
{
    private readonly IEventSource? _eventSource;
    private readonly IAdapterRepository? _repository;
    private readonly ITextFormatter? _textFormatter;
    private readonly ITextTokenizer? _textTokenizer;
    private readonly IMessageService? _messageService;
    private readonly IMemberService? _memberService;
    private readonly IChannelService? _channelService;

    protected IAttachmentCache AttachmentCache { get; }
    protected IAdapterEventHandler EventHandler { get; }

    protected Adapter(AdapterIdentifier adapterId, AdapterContext context)
    {
        Id = adapterId;

        AttachmentCache = context.AttachmentCache ?? DefaultAttachmentCache.Instance;
        EventHandler = context.EventHandler ?? DefaultEventHandler.Instance;
    }

    public AdapterIdentifier Id { get; }
    public abstract string DisplayName { get; }

    public AdapterFeatures Features { get; private set; }

    public IEventSource EventSource
    {
        get => GetFeature(_eventSource, AdapterFeatures.EventSource);
        protected init => SetFeature(out _eventSource, value, AdapterFeatures.EventSource);
    }

    public IAdapterRepository Repository
    {
        get => GetFeature(_repository, AdapterFeatures.Repository);

        protected init => SetFeature(out _repository,
            new CommonAdapterRepository(this, value),
            AdapterFeatures.Repository);
    }

    public ITextFormatter TextFormatter
    {
        get => GetFeature(_textFormatter, AdapterFeatures.TextFormatter);
        protected init => SetFeature(out _textFormatter, value, AdapterFeatures.TextFormatter);
    }

    public ITextTokenizer TextTokenizer
    {
        get => GetFeature(_textTokenizer, AdapterFeatures.TextTokenizer);
        protected init => SetFeature(out _textTokenizer, value, AdapterFeatures.TextTokenizer);
    }

    public IMessageService MessageService
    {
        get => GetFeature(_messageService, AdapterFeatures.MessageService);

        protected init => SetFeature(out _messageService,
            new CommonMessageService(this, AttachmentCache, value),
            AdapterFeatures.MessageService);
    }

    public IMemberService MemberService
    {
        get => GetFeature(_memberService, AdapterFeatures.MemberService);

        protected init => SetFeature(out _memberService,
            new CommonMemberService(this, value),
            AdapterFeatures.MemberService);
    }

    public IChannelService ChannelService
    {
        get => GetFeature(_channelService, AdapterFeatures.ChannelService);

        protected init => SetFeature(out _channelService,
            new CommonChannelService(this, value),
            AdapterFeatures.ChannelService);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private T GetFeature<T>(T? instance, AdapterFeatures feature)
    {
        if (instance is null)
        {
            throw new UnsupportedFeatureException(Id, feature);
        }

        return instance;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void SetFeature<T>(out T field, T instance, AdapterFeatures feature)
    {
        field = instance;
        Features |= feature;
    }
}
