using Kantaiko.Properties;
using Replikit.Core.Routing.Context;

namespace Replikit.Core.Routing;

internal class ApplicationBuilder : IApplicationBuilder
{
    private BotEventDelegate _eventDelegate = DefaultEventDelegate;

    public ApplicationBuilder(IServiceProvider applicationServices)
    {
        ApplicationServices = applicationServices;
    }

    public IServiceProvider ApplicationServices { get; }
    public IPropertyCollection Properties { get; } = new PropertyCollection();

    public void Use(Func<BotEventDelegate, BotEventDelegate> middleware)
    {
        _eventDelegate = middleware(_eventDelegate);
    }

    public BotEventDelegate Build()
    {
        return _eventDelegate;
    }

    private static Task DefaultEventDelegate(IBotEventContext context)
    {
        return Task.CompletedTask;
    }
}
