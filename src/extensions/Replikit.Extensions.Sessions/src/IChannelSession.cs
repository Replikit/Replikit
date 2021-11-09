namespace Replikit.Extensions.Sessions;

public interface IChannelSession<TValue> : ISession<TValue> where TValue : class, new() { }
