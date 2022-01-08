using Kantaiko.Properties;

namespace Replikit.Extensions.Views;

public record ViewActionEndpointProperties(bool IsAction, bool IsExternalActivationAllowed) :
    ReadOnlyPropertiesBase<ViewActionEndpointProperties>;
