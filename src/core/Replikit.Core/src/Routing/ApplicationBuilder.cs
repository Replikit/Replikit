using Kantaiko.Properties;
using Replikit.Abstractions.Events;
using Replikit.Core.Routing.Context;

namespace Replikit.Core.Routing;

internal class ApplicationBuilder : IApplicationBuilder
{
    private AdapterEventDelegate _eventDelegate = DefaultEventDelegate;

    public ApplicationBuilder(IServiceProvider applicationServices)
    {
        ApplicationServices = applicationServices;
    }

    public IServiceProvider ApplicationServices { get; }
    public IPropertyCollection Properties { get; } = new PropertyCollection();

    public void Use(Func<AdapterEventDelegate, AdapterEventDelegate> middleware)
    {
        _eventDelegate = middleware(_eventDelegate);
    }

    public AdapterEventDelegate Build()
    {
        return _eventDelegate;
    }

    private static Task DefaultEventDelegate(IAdapterEventContext<IAdapterEvent> context)
    {
        return Task.CompletedTask;
    }
}
