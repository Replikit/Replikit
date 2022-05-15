using MongoDB.Driver;

namespace Replikit.Integrations.MongoDB.Options;

public class ReplikitMongoOptions
{
    public Func<IServiceProvider, IMongoDatabase>? DatabaseFactory { get; set; }

    public string StateCollectionName { get; set; } = "States";
    public string UserCollectionName { get; set; } = "Users";
}
