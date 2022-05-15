using Kantaiko.Hosting.Lifecycle;
using Kantaiko.Hosting.Lifecycle.Events;
using Kantaiko.Routing;
using Kantaiko.Routing.Events;
using Microsoft.EntityFrameworkCore;

namespace Replikit.Examples.EntityFrameworkCore.LifecycleHandlers.ApplicationStarted;

internal class MigrateDatabaseHandler : LifecycleEventHandler<ApplicationStartedEvent>
{
    private readonly ApplicationDbContext _dbContext;

    public MigrateDatabaseHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected override async Task<Unit> HandleAsync(IEventContext<ApplicationStartedEvent> context)
    {
        await _dbContext.Database.MigrateAsync(context.CancellationToken);

        return default;
    }
}
