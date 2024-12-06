using Bidro.Config.LocationComponents;
using Bidro.FrontEndBuildBlocks.Categories;
using Bidro.FrontEndBuildBlocks.FormQuestion;
using Bidro.Listings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Bidro.Config;

public class EntityDbContext(DbContextOptions<EntityDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Subcategory> Subcategories { get; set; }
    public DbSet<County> Counties { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<ListingComponents.Location> Locations { get; set; }
    public DbSet<ListingComponents.Contact> Contacts { get; set; }
    public DbSet<ListingComponents.FormAnswer> FormAnswers { get; set; }
    public DbSet<Listing> Listings { get; set; }
    public DbSet<FormQuestion> FormQuestions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Category>().HasKey(c => c.Id);
        modelBuilder.Entity<Category>().Property(c => c.Name).IsRequired();
        modelBuilder.Entity<Category>().Property(c => c.Icon).IsRequired();
        modelBuilder.Entity<Category>().HasMany(c => c.Subcategories).WithOne(s => s.ParentCategory);

        modelBuilder.Entity<Subcategory>().HasKey(s => s.Id);
        modelBuilder.Entity<Subcategory>().Property(s => s.Name).IsRequired();
        modelBuilder.Entity<Subcategory>().Property(s => s.Icon).IsRequired();
        modelBuilder.Entity<Subcategory>()
            .HasOne(s => s.ParentCategory)
            .WithMany(c => c.Subcategories);

        modelBuilder.Entity<County>()
            .HasKey(c => c.Id);
        modelBuilder.Entity<County>()
            .Property(c => c.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();
        modelBuilder.Entity<County>()
            .Property(c => c.Name)
            .IsRequired();
        modelBuilder.Entity<County>()
            .HasMany(c => c.Cities)
            .WithOne(c => c.County);
        modelBuilder.Entity<County>()
            .HasMany(c => c.Locations)
            .WithOne(c => c.County);
        modelBuilder.Entity<County>()
            .Property(c => c.Code)
            .HasMaxLength(2)
            .IsRequired();

        modelBuilder.Entity<City>().HasKey(c => c.Id);
        modelBuilder.Entity<City>().Property(c => c.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();
        modelBuilder.Entity<City>().Property(c => c.Name).IsRequired();
        modelBuilder.Entity<City>().HasOne(c => c.County).WithMany(c => c.Cities);
        modelBuilder.Entity<City>().HasMany(c => c.Locations).WithOne(c => c.City);

        modelBuilder.Entity<ListingComponents.Location>().HasKey(l => l.ListingId);
        modelBuilder.Entity<ListingComponents.Location>().HasOne(l => l.Listing).WithOne(l => l.Location);
        modelBuilder.Entity<ListingComponents.Location>().HasOne(l => l.County).WithMany(c => c.Locations);
        modelBuilder.Entity<ListingComponents.Location>().HasOne(l => l.City).WithMany(c => c.Locations);

        modelBuilder.Entity<ListingComponents.Contact>().HasKey(c => c.ListingId);
        modelBuilder.Entity<ListingComponents.Contact>().HasOne(c => c.Listing).WithOne(l => l.Contact);

        modelBuilder.Entity<ListingComponents.FormAnswer>().HasKey(f => f.Id);
        modelBuilder.Entity<ListingComponents.FormAnswer>()
            .HasOne(f => f.Listing)
            .WithMany(l => l.FormAnswers);
        modelBuilder.Entity<ListingComponents.FormAnswer>()
            .HasOne(f => f.Question)
            .WithMany(q => q.Answers);

        modelBuilder.Entity<Listing>().HasKey(l => l.Id);
        modelBuilder.Entity<Listing>().Property(l => l.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();
        modelBuilder.Entity<Listing>().HasOne(l => l.Location).WithOne(l => l.Listing);
        modelBuilder.Entity<Listing>().HasOne(l => l.Contact).WithOne(l => l.Listing);
        modelBuilder.Entity<Listing>().HasMany(l => l.FormAnswers).WithOne(f => f.Listing);

        modelBuilder.Entity<FormQuestion>().HasKey(q => q.Id);
        modelBuilder.Entity<FormQuestion>().Property(q => q.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();
        modelBuilder.Entity<FormQuestion>().Property(q => q.Label).IsRequired();
        modelBuilder.Entity<FormQuestion>().Property(q => q.Value).IsRequired();
        modelBuilder.Entity<FormQuestion>().HasMany(q => q.Answers)
            .WithOne(a => a.Question)
            .HasForeignKey(a => a.FormQuestionId);
        modelBuilder.Entity<FormQuestion>().HasOne(q => q.Subcategory).WithMany(s => s.FormQuestions);
    }
}

public class EntityDbContextFactory : IDesignTimeDbContextFactory<EntityDbContext>
{
    public EntityDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EntityDbContext>();
        optionsBuilder.UseNpgsql(@"Host=localhost;Database=bidro;Username=postgres;Password=admin");
            
        return new EntityDbContext(optionsBuilder.Options);
    }
}