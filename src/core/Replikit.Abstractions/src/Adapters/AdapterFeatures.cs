namespace Replikit.Abstractions.Adapters;

[Flags]
public enum AdapterFeatures
{
    None = 0,
    EventSource = 1 << 0,
    MessageService = 1 << 1,
    TextFormatter = 1 << 2,
    TextTokenizer = 1 << 3,
    Repository = 1 << 4,
    MemberService = 1 << 5,
    ChannelService = 1 << 6,
    All = ~0
}
