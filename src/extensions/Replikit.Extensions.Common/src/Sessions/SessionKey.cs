using Replikit.Abstractions.Common.Models;

namespace Replikit.Extensions.Common.Sessions;

public record SessionKey
{
    public SessionKey(Type modelType, SessionType type, AdapterIdentifier? adapterId = null,
        Identifier? channelId = null, Identifier? accountId = null, long? userId = null)
    {
        Model = modelType.FullName!;
        Type = type;
        AdapterId = adapterId;
        ChannelId = channelId;
        AccountId = accountId;
        UserId = userId;
    }

    public string Model { get; private set; }
    public SessionType Type { get; private set; }
    public AdapterIdentifier? AdapterId { get; private set; }
    public Identifier? ChannelId { get; private set; }
    public Identifier? AccountId { get; private set; }
    public long? UserId { get; private set; }
}
