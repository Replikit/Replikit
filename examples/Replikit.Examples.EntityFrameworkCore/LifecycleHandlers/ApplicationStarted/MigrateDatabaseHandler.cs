using Kantaiko.Hosting.Lifecycle.Events;
using Kantaiko.Routing.Events;
using Microsoft.EntityFrameworkCore;

namespace Replikit.Examples.EntityFrameworkCore.LifecycleHandlers.ApplicationStarted;

internal class MigrateDatabaseHandler : AsyncEventHandlerBase<ApplicationStartedEvent>
{
    private readonly ApplicationDbContext _dbContext;

    public MigrateDatabaseHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected override Task HandleAsync(IAsyncEventContext<ApplicationStartedEvent> context)
    {
        return _dbContext.Database.MigrateAsync(context.CancellationToken);
    }
}
