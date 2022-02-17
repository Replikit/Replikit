using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Common.Models;
using Replikit.Adapters.Common.Adapters;
using Replikit.Adapters.Telegram.Internal;
using Replikit.Adapters.Telegram.Services;
using Telegram.Bot;

namespace Replikit.Adapters.Telegram;

internal class TelegramAdapter : Adapter
{
    public override string DisplayName => "Telegram";

    public TelegramAdapter(AdapterIdentifier identifier, AdapterContext context, ITelegramBotClient backend,
        TelegramAdapterOptions options) : base(identifier, context)
    {
        TextFormatter = new TelegramTextFormatter();
        TextTokenizer = new TelegramTextTokenizer();

        var entityFactory = new TelegramEntityFactory(this);

        var repository = new TelegramAdapterRepository(options.Token, backend, entityFactory);
        Repository = repository;

        var messageResolver = new TelegramMessageResolver(Id, TextFormatter, repository, AttachmentCache);

        EventSource = new TelegramEventSource(this, EventHandler, backend, repository, entityFactory);
        MessageService = new TelegramMessageService(backend, messageResolver, entityFactory);

        MemberService = new TelegramMemberService(backend);
        ChannelService = new TelegramChannelService(backend, messageResolver);
    }
}
