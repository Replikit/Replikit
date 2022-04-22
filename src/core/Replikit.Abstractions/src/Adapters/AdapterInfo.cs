using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Repositories.Models;

namespace Replikit.Abstractions.Adapters;

public record AdapterInfo(AdapterIdentifier AdapterId, AccountInfo BotAccount, string DisplayName);
