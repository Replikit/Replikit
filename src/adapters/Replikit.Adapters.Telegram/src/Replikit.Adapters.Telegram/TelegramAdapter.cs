using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Adapters.Factory;
using Replikit.Abstractions.Common.Models;
using Replikit.Adapters.Common.Adapters;
using Replikit.Adapters.Telegram.Abstractions;
using Replikit.Adapters.Telegram.Internal;
using Replikit.Adapters.Telegram.Services;
using Telegram.Bot;

namespace Replikit.Adapters.Telegram;

internal class TelegramAdapter : Adapter, ITelegramAdapter
{
    public const string Type = "telegram";

    private readonly TelegramEntityFactory _entityFactory;

    public TelegramAdapter(AdapterInfo adapterInfo, PlatformInfo platformInfo, AdapterFactoryContext context,
        ITelegramBotClient backend, TelegramAdapterOptions options) :
        base(adapterInfo, platformInfo, context)
    {
        Backend = backend;

        TextFormatter = new TelegramTextFormatter();
        TextTokenizer = new TelegramTextTokenizer();

        _entityFactory = new TelegramEntityFactory(this);

        var messageResolver = new TelegramMessageResolver(TextFormatter, AttachmentCache);

        AdapterEventSource = new TelegramEventSource(this, EventDispatcher, Backend, _entityFactory);
        MessageService = new TelegramMessageService(this, Backend, messageResolver, _entityFactory);

        AccountService = new TelegramAccountService(Backend, _entityFactory);
        ChannelService = new TelegramChannelService(Backend, _entityFactory);
        MemberService = new TelegramMemberService(Backend);

        AttachmentService = new TelegramAttachmentService(Backend, options.Token);
    }

    protected override async Task<AdapterBotInfo> InitializeAsync(CancellationToken cancellationToken)
    {
        var bot = await Backend.GetMeAsync(cancellationToken);
        var botId = new BotIdentifier(PlatformInfo.Id, bot.Id);

        var account = _entityFactory.CreateAccountInfo(bot, botId);

        return new AdapterBotInfo(botId, account);
    }

    public ITelegramBotClient Backend
    {
        get => this.GetRequiredService<ITelegramBotClient>();
        private init => SetService(value);
    }
}
