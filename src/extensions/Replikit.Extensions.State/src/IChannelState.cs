namespace Replikit.Extensions.State;

public interface IChannelState<TValue> : IState<TValue> where TValue : notnull, new() { }
