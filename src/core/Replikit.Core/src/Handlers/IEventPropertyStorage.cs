namespace Replikit.Core.Handlers;

public interface IEventPropertyStorage
{
    Dictionary<string, object> Properties { get; }
}
