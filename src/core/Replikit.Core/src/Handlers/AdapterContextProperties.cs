using Kantaiko.Properties;
using Replikit.Abstractions.Adapters;

namespace Replikit.Core.Handlers;

public record AdapterContextProperties(IAdapter Adapter) : ReadOnlyPropertiesBase<AdapterContextProperties>;
