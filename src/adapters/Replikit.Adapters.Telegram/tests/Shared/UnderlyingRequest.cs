using Telegram.Bot.Requests.Abstractions;

namespace Replikit.Adapters.Telegram.Tests.Shared;

internal record struct UnderlyingRequest(IRequest Request, object Response);
