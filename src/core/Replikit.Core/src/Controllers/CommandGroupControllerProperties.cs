using Kantaiko.Properties;

namespace Replikit.Core.Controllers;

public record CommandGroupControllerProperties(string CommandGroupName) :
    ReadOnlyPropertiesBase<CommandGroupControllerProperties>;
