using Kantaiko.Properties;

namespace Replikit.Core.Controllers;

public record CommandParameterProperties(IReadOnlyList<string> ParameterNames) :
    ReadOnlyPropertiesBase<CommandParameterProperties>;
