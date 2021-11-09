namespace Replikit.Extensions.Sessions;

public interface IAccountSession<TValue> : ISession<TValue> where TValue : class, new() { }
