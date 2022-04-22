using Kantaiko.Properties;
using Replikit.Extensions.State.Implementation;

namespace Replikit.Extensions.State.Context;

public record StateContextProperties(
    IStateKeyFactory StateKeyFactory
) : ReadOnlyPropertiesBase<StateContextProperties>;
