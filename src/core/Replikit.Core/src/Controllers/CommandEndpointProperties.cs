using Kantaiko.Properties;

namespace Replikit.Core.Controllers;

public record CommandEndpointProperties(string CommandName, IReadOnlyList<string> Aliases) :
    ReadOnlyPropertiesBase<CommandEndpointProperties>;
