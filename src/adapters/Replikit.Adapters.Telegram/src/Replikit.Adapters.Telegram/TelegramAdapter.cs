using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Adapters.Services;
using Replikit.Abstractions.Common.Models;
using Replikit.Adapters.Common.Adapters;
using Replikit.Adapters.Telegram.Abstractions;
using Replikit.Adapters.Telegram.Internal;
using Replikit.Adapters.Telegram.Services;
using Telegram.Bot;

namespace Replikit.Adapters.Telegram;

internal class TelegramAdapter : Adapter, ITelegramAdapter
{
    public TelegramAdapter(AdapterIdentifier id, AdapterFactoryContext context,
        ITelegramBotClient backend, TelegramAdapterOptions options) :
        base(id, context)
    {
        Backend = backend;

        TextFormatter = new TelegramTextFormatter();
        TextTokenizer = new TelegramTextTokenizer();

        var entityFactory = new TelegramEntityFactory(id);

        var repository = new TelegramAdapterRepository(options, Backend, entityFactory);
        Repository = repository;

        var messageResolver = new TelegramMessageResolver(Id, TextFormatter, repository, AttachmentCache);

        EventSource = new TelegramEventSource(this, EventHandler, Backend, repository, entityFactory);
        MessageService = new TelegramMessageService(Backend, messageResolver, entityFactory);

        MemberService = new TelegramMemberService(Backend);
        ChannelService = new TelegramChannelService(Backend, messageResolver);
    }

    public ITelegramBotClient Backend
    {
        get => this.GetRequiredService<ITelegramBotClient>();
        private init => SetService(value);
    }
}
