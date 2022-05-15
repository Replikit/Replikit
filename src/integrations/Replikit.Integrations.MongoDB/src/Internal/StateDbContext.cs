using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Replikit.Core.Abstractions.State;
using Replikit.Integrations.MongoDB.Options;

namespace Replikit.Integrations.MongoDB.Internal;

internal class StateDbContext
{
    public IMongoCollection<StateItem> States { get; }

    public StateDbContext(RootDbContext dbContext, IOptions<ReplikitMongoOptions> options)
    {
        States = dbContext.Database.GetCollection<StateItem>(options.Value.StateCollectionName);
    }
}
