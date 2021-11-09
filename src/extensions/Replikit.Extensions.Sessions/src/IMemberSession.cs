namespace Replikit.Extensions.Sessions;

public interface IMemberSession<TValue> : ISession<TValue> where TValue : class, new() { }
