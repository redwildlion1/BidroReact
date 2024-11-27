using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Bidro.Config;

public class EntityDbContext : DbContext
{
    [RequiresDynamicCode("This method is called by Entity Framework Core. Do not call it directly.")]
    [RequiresUnreferencedCode("This method is called by Entity Framework Core. Do not call it directly.")]
    public EntityDbContext(DbContextOptions<EntityDbContext> options) : base(options)
    {
        SavingChanges += (sender, args) =>
        {
            Console.WriteLine($"Saving changes for {((DbContext)sender!).Database.
                GetConnectionString()}");
        };
        SavedChanges += (sender, args) =>
        {
            Console.WriteLine($"Saved {args.EntitiesSavedCount} entities");
        };
        SaveChangesFailed += (sender, args) =>
        {
            Console.WriteLine($"An exception occurred! {args.Exception.Message} entities");
        };
    }

    [UnconditionalSuppressMessage("Trimming", "IL2026:Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code", Justification = "<Pending>")]
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EntityDbContext).Assembly);
    }
    
}

[RequiresUnreferencedCode("This class is used by Entity Framework Core. Do not remove it.")]
[RequiresDynamicCode("This class is used by Entity Framework Core. Do not remove it.")]
public class EntityDbContextFactory : IDesignTimeDbContextFactory<EntityDbContext>
{
    [RequiresUnreferencedCode("This method is called by Entity Framework Core. Do not call it directly.")]
    [RequiresDynamicCode("This method is called by Entity Framework Core. Do not call it directly.")]
    public EntityDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EntityDbContext>();
        var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
        optionsBuilder.UseNpgsql(connectionString);

        return new EntityDbContext(optionsBuilder.Options);
    }
}