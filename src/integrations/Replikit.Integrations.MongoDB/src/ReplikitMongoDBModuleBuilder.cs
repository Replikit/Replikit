using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Driver;
using Replikit.Core.Abstractions.State;
using Replikit.Core.Abstractions.Users;
using Replikit.Integrations.MongoDB.Internal;
using Replikit.Integrations.MongoDB.Options;
using Replikit.Integrations.MongoDB.State;
using Replikit.Integrations.MongoDB.Users;

namespace Replikit.Integrations.MongoDB;

// ReSharper disable once InconsistentNaming
public class ReplikitMongoDBModuleBuilder
{
    private readonly IServiceCollection _services;

    public ReplikitMongoDBModuleBuilder(IServiceCollection services)
    {
        _services = services;
    }

    public ReplikitMongoDBModuleBuilder AddDatabase(Func<IServiceProvider, IMongoDatabase> databaseFactory)
    {
        _services.Configure<ReplikitMongoOptions>(options => options.DatabaseFactory = databaseFactory);

        return this;
    }

    public ReplikitMongoDBModuleBuilder AddDatabase(IMongoDatabase database)
    {
        _services.Configure<ReplikitMongoOptions>(options => options.DatabaseFactory = _ => database);

        return this;
    }

    public ReplikitMongoDBModuleBuilder AddDatabase(string connectionString)
    {
        _services.Configure<ReplikitMongoOptions>(options => options.DatabaseFactory = DatabaseFactory);

        return this;

        IMongoDatabase DatabaseFactory(IServiceProvider _)
        {
            var url = new MongoUrl(connectionString);

            if (string.IsNullOrWhiteSpace(url.DatabaseName))
            {
                throw new InvalidOperationException("Connection string does not contain the database name");
            }

            var client = new MongoClient(url);

            return client.GetDatabase(url.DatabaseName);
        }
    }

    public ReplikitMongoDBModuleBuilder AddDefaults()
    {
        AddStateStore();
        AddUserStore<ReplikitUser, Guid>();

        return this;
    }

    public ReplikitMongoDBModuleBuilder AddStateStore()
    {
        _services.AddSingleton<StateDbContext>();
        _services.Replace(ServiceDescriptor.Singleton<IStateStore, StateStore>());

        return this;
    }

    public ReplikitMongoDBModuleBuilder AddUserStore<TUser, TUserId>() where TUser : ReplikitUser<TUserId>
    {
        _services.AddSingleton<UserDbContext<TUser, TUserId>>();
        _services.Replace(ServiceDescriptor.Singleton<IUserStore<TUser, TUserId>, UserStore<TUser, TUserId>>());

        return this;
    }
}
