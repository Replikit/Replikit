using Kantaiko.Controllers.Introspection;

namespace Replikit.Core.Controllers;

public interface IControllerIntrospectionInfoAccessor
{
    IntrospectionInfo IntrospectionInfo { get; }
}
