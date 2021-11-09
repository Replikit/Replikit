namespace Replikit.Extensions.Sessions;

public interface IUserSession<TValue> : ISession<TValue> where TValue : class, new() { }
