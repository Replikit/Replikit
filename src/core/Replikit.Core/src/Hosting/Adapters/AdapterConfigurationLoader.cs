using Microsoft.Extensions.Configuration;
using Replikit.Abstractions.Adapters.Loader;
using Replikit.Core.Options;

namespace Replikit.Core.Hosting.Adapters;

internal class AdapterConfigurationLoader
{
    private const string AdapterSectionKey = "Replikit:Adapters";

    public static void LoadAdaptersFromConfiguration(AdapterLoaderOptions options, IConfiguration configuration)
    {
        var adapters = configuration.GetSection(AdapterSectionKey);
        if (!adapters.Exists()) return;

        foreach (var descriptorConfiguration in adapters.GetChildren())
        {
            var adapterType = descriptorConfiguration.GetValue<string>("Type");
            if (adapterType is null) continue;

            var adapterFactory = options.AdapterFactories.GetValueOrDefault(adapterType);

            object optionsInstance;

            if (adapterFactory is not null)
            {
                optionsInstance = Activator.CreateInstance(adapterFactory.OptionsType)!;
                descriptorConfiguration.Bind(optionsInstance);
            }
            else
            {
                optionsInstance = new object();
            }

            options.AdapterDescriptors.Add(new AdapterDescriptor(adapterType, optionsInstance));
        }
    }
}
