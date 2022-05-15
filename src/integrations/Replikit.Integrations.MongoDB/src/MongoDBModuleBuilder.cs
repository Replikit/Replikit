using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Replikit.Core.Abstractions.State;
using Replikit.Core.Abstractions.Users;
using Replikit.Integrations.MongoDB.Internal;
using Replikit.Integrations.MongoDB.Options;
using Replikit.Integrations.MongoDB.State;
using Replikit.Integrations.MongoDB.Users;

namespace Replikit.Integrations.MongoDB;

// ReSharper disable once InconsistentNaming
public class MongoDBModuleBuilder
{
    private readonly IServiceCollection _services;

    public MongoDBModuleBuilder(IServiceCollection services)
    {
        _services = services;
    }

    public MongoDBModuleBuilder AddDatabase(Func<IServiceProvider, IMongoDatabase> databaseFactory)
    {
        _services.Configure<ReplikitMongoOptions>(options => options.DatabaseFactory = databaseFactory);

        return this;
    }

    public MongoDBModuleBuilder AddDatabase(IMongoDatabase database)
    {
        _services.Configure<ReplikitMongoOptions>(options => options.DatabaseFactory = _ => database);

        return this;
    }

    public MongoDBModuleBuilder AddDatabase(string connectionString)
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

    public MongoDBModuleBuilder AddDefaults()
    {
        AddStateStore();
        AddUserStore<ReplikitUser, Guid>();

        return this;
    }

    public MongoDBModuleBuilder AddStateStore()
    {
        _services.AddSingleton<StateDbContext>();
        _services.AddScoped<IStateStore, StateStore>();

        return this;
    }

    public MongoDBModuleBuilder AddUserStore<TUser, TUserId>() where TUser : ReplikitUser<TUserId>
    {
        _services.AddSingleton<UserDbContext<TUser, TUserId>>();
        _services.AddScoped<IUserStore<TUser, TUserId>, UserStore<TUser, TUserId>>();

        return this;
    }
}
