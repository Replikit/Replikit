using Microsoft.EntityFrameworkCore;
using Replikit.Examples.EntityFrameworkCore.Entities;
using Replikit.Integrations.EntityFrameworkCore;

namespace Replikit.Examples.EntityFrameworkCore;

internal class ApplicationDbContext : ReplikitDbContext<ApplicationUser, long>
{
    public ApplicationDbContext(DbContextOptions options, IServiceProvider serviceProvider) :
        base(options, serviceProvider) { }
}
