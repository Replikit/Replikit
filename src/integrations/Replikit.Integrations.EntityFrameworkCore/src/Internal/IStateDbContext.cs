using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Replikit.Core.Abstractions.State;

namespace Replikit.Integrations.EntityFrameworkCore.Internal;

internal interface IStateDbContext : IDbContext
{
    DbSet<StateItem> States { get; }
}
