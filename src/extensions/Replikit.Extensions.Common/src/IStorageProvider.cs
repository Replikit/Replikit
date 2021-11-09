namespace Replikit.Extensions.Common;

public interface IStorageProvider<out TStorage>
{
    TStorage Resolve(string? type = null);
}
