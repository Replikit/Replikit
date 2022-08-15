using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Replikit.Integrations.MongoDB.Options;

namespace Replikit.Integrations.MongoDB.Internal;

internal class RootDbContext
{
    public IMongoDatabase Database { get; }

    public RootDbContext(IOptions<ReplikitMongoOptions> options, IServiceProvider serviceProvider)
    {
        if (options.Value.DatabaseFactory is null)
        {
            throw new InvalidOperationException("MongoDatabase factory is not specified");
        }

        Database = options.Value.DatabaseFactory.Invoke(serviceProvider);
    }
}
