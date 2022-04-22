using Kantaiko.Properties;

namespace Replikit.Extensions.Views;

public record ViewActionEndpointProperties(
    bool IsAction,
    bool IsExternalActivationAllowed,
    bool AutoUpdate
) : ReadOnlyPropertiesBase<ViewActionEndpointProperties>;
