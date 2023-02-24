namespace Replikit.Core.Sessions;

public class SessionOptions
{
    public TimeSpan? SlidingCacheExpiration { get; set; }
    public TimeSpan? AbsoluteCacheExpiration { get; set; }
}
