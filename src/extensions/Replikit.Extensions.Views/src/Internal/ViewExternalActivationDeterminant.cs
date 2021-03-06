using Kantaiko.Controllers.Introspection;

namespace Replikit.Extensions.Views.Internal;

internal class ViewExternalActivationDeterminant
{
    private readonly Dictionary<ControllerInfo, bool> _states = new();

    public bool IsExternalActivationAllowed(ControllerInfo controllerInfo)
    {
        if (_states.TryGetValue(controllerInfo, out var state)) return state;

        return _states[controllerInfo] = controllerInfo.Endpoints.Any(IsExternalActivationAllowed);
    }

    public bool IsExternalActivationAllowed(EndpointInfo endpoint)
    {
        return ViewActionEndpointProperties.Of(endpoint)?.IsExternalActivationAllowed is true;
    }
}
