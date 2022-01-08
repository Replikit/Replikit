using Kantaiko.Controllers.Introspection;
using Replikit.Abstractions.Common.Exceptions;

namespace Replikit.Extensions.Views.Exceptions;

public class ExternalActivationNotAllowedException : ReplikitException
{
    public ExternalActivationNotAllowedException(EndpointInfo endpointInfo) : base(
        $"External activation of the method \"{endpointInfo.MethodInfo.Name}\" " +
        $"of view \"{endpointInfo.Controller!.Type.Name}\" is not allowed") { }
}
