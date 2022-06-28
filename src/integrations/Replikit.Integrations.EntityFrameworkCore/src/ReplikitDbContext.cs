using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Abstractions.State;
using Replikit.Core.Abstractions.Users;
using Replikit.Core.Common;
using Replikit.Core.Serialization;
using Replikit.Integrations.EntityFrameworkCore.Internal;
using Replikit.Integrations.EntityFrameworkCore.Serialization;
using Replikit.Integrations.EntityFrameworkCore.Utils;

namespace Replikit.Integrations.EntityFrameworkCore;

public class ReplikitDbContext<TUser, TUserId> : DbContext, IStateDbContext, IUserDbContext<TUser, TUserId>
    where TUser : ReplikitUser<TUserId>
{
    private readonly IServiceProvider _serviceProvider;

    protected ReplikitDbContext(DbContextOptions options, IServiceProvider serviceProvider) : base(options)
    {
        _serviceProvider = serviceProvider;
    }

    public virtual DbSet<StateItem> States => Set<StateItem>();

    public virtual DbSet<TUser> Users => Set<TUser>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var options = new JsonSerializerOptions();

        options.Converters.Add(new DynamicValueConverter());
        options.Converters.Add(new TypeConverter());
        options.AddReplikitConverters();

        modelBuilder.Entity<StateItem>(builder =>
        {
            builder.HasKey(x => x.Key);

            builder.Property(x => x.Key)
                .HasJsonConversion(options);

            builder.Property(x => x.Value)
                .HasConversion(
                    x => JsonSerializer.Serialize(x, options),
                    x => JsonSerializer.Deserialize<DynamicValue>(x, options)!
                );
        });

        modelBuilder.Entity<TUser>(builder =>
        {
            builder.Property(x => x.AccountIds)
                .HasJsonConversion(options);
        });

        // Exclude entities from the model when corresponding modules are not installed

        if (_serviceProvider.GetService<IEntityUsageIndicator<ReplikitUser>>() is null)
        {
            modelBuilder.Ignore<TUser>();
        }

        if (_serviceProvider.GetService<IEntityUsageIndicator<StateItem>>() is null)
        {
            modelBuilder.Ignore<StateItem>();
        }
    }
}
