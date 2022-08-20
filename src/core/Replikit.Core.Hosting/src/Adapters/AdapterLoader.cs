using System.Drawing;
using Kantaiko.ConsoleFormatting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Replikit.Abstractions.Adapters.Factory;
using Replikit.Core.Hosting.Options;

namespace Replikit.Core.Hosting.Adapters;

internal class AdapterLoader
{
    private readonly AdapterLoaderOptions _options;
    private readonly ILogger<AdapterLoader> _logger;
    private readonly AdapterCollection _adapterCollection;

    public AdapterLoader(ILogger<AdapterLoader> logger, IOptions<AdapterLoaderOptions> options,
        AdapterCollection adapterCollection)
    {
        _logger = logger;
        _adapterCollection = adapterCollection;
        _options = options.Value;
    }

    public async Task LoadAdapters(AdapterFactoryContext factoryContext,
        CancellationToken cancellationToken = default)
    {
        foreach (var descriptor in _options.AdapterDescriptors)
        {
            var adapterFactory = _options.AdapterFactories.GetValueOrDefault(descriptor.Type);

            if (adapterFactory is null)
            {
                _logger.LogWarning(
                    "Adapter with type \"{AdapterType}\" were configured, but no corresponding factory was registered",
                    descriptor.Type);

                continue;
            }

            var adapter = await adapterFactory.CreateAsync(descriptor.Options, factoryContext, cancellationToken);
            _adapterCollection.Add(adapter);

            _logger.LogInformation("Loaded adapter {AdapterType} [Id = {BotId}]",
                Colors.FgColor(adapter.AdapterInfo.DisplayName, Color.Cyan),
                Colors.FgColor(adapter.BotInfo.Account.Id.Value.ToString()!, Color.LightCyan));
        }

        if (_options.AdapterDescriptors.Count == 0)
        {
            _logger.LogWarning("No adapters were configured");
        }
    }
}
