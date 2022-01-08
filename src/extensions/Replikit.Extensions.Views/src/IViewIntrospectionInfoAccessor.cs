using Kantaiko.Controllers.Introspection;

namespace Replikit.Extensions.Views;

public interface IViewIntrospectionInfoAccessor
{
    IntrospectionInfo IntrospectionInfo { get; }
}
