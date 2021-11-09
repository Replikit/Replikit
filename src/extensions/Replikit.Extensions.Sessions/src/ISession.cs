namespace Replikit.Extensions.Sessions;

public interface ISession<TValue> where TValue : class, new()
{
    TValue Value { get; }
}
