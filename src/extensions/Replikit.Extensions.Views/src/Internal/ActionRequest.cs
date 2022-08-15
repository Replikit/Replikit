using Kantaiko.Controllers.Introspection;

namespace Replikit.Extensions.Views.Internal;

internal class ActionRequest
{
    public ActionRequest(EndpointInfo endpoint, IReadOnlyList<object> parameters)
    {
        Endpoint = endpoint;
        Parameters = parameters;
    }

    public EndpointInfo Endpoint { get; }
    public IReadOnlyList<object> Parameters { get; }
}
