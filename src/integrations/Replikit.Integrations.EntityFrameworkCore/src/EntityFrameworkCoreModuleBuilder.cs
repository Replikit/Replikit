using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Abstractions.State;
using Replikit.Core.Abstractions.Users;
using Replikit.Integrations.EntityFrameworkCore.Internal;
using Replikit.Integrations.EntityFrameworkCore.State;
using Replikit.Integrations.EntityFrameworkCore.Users;

namespace Replikit.Integrations.EntityFrameworkCore;

public class EntityFrameworkCoreModuleBuilder
{
    private Type? _dbContextType;

    private readonly IServiceCollection _services;

    public EntityFrameworkCoreModuleBuilder(IServiceCollection services)
    {
        _services = services;
    }

    public EntityFrameworkCoreModuleBuilder AddDbContext<TDbContext>() where TDbContext : DbContext
    {
        _dbContextType = typeof(TDbContext);

        return this;
    }

    public EntityFrameworkCoreModuleBuilder AddDefaults()
    {
        AddStateStore();
        AddUserStore<ReplikitUser, Guid>();

        return this;
    }

    public EntityFrameworkCoreModuleBuilder AddStateStore()
    {
        if (_dbContextType is null)
        {
            throw new InvalidOperationException("DbContext is not configured");
        }

        _services.AddTransient(sp => (IStateDbContext) sp.GetRequiredService(_dbContextType));

        _services.AddScoped<IStateStore, StateStore>();

        return this;
    }

    public EntityFrameworkCoreModuleBuilder AddUserStore<TUser, TUserId>() where TUser : ReplikitUser<TUserId>
    {
        if (_dbContextType is null)
        {
            throw new InvalidOperationException("DbContext is not configured");
        }

        _services.AddTransient(sp => (IUserDbContext<TUser, TUserId>) sp.GetRequiredService(_dbContextType));

        _services.AddScoped<IUserStore<TUser, TUserId>, UserStore<TUser, TUserId>>();

        return this;
    }
}
