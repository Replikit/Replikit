namespace Replikit.Extensions.Sessions;

public interface IGlobalSession<TValue> : ISession<TValue> where TValue : class, new() { }
