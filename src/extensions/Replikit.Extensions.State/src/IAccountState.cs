namespace Replikit.Extensions.State;

public interface IAccountState<TValue> : IState<TValue> where TValue : notnull, new() { }
