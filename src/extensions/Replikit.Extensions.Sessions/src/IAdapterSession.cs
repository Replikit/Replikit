namespace Replikit.Extensions.Sessions;

public interface IAdapterSession<TValue> : ISession<TValue> where TValue : class, new() { }
