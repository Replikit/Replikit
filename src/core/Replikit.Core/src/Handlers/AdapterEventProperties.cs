using Kantaiko.Properties;
using Replikit.Abstractions.Adapters;

namespace Replikit.Core.Handlers;

public record AdapterEventProperties(IAdapter Adapter) : ReadOnlyPropertiesBase<AdapterEventProperties>;
