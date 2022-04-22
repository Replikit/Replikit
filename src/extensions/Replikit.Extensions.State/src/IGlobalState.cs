namespace Replikit.Extensions.State;

public interface IGlobalState<TValue> : IState<TValue> where TValue : notnull, new() { }
