using Bidro.EntityObjects;
using Bidro.Services.Implementations;
using Bidro.Types;
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
    public DbSet<Firm> Firms { get; set; }
    public DbSet<FirmLocation> FirmLocations { get; set; }
    public DbSet<FirmContact> FirmContacts { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<UserTypes.FirmAccount> FirmAccounts { get; set; }
    public DbSet<UserTypes.UserAccount> UserAccounts { get; set; }
    public DbSet<UserTypes.AdminAccount> AdminAccounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasKey(c => c.Id);
        modelBuilder.Entity<Category>().Property(c => c.Name)
            .IsRequired();
        modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
        modelBuilder.Entity<Category>().Property(c => c.Icon).IsRequired();
        modelBuilder.Entity<Category>().HasMany(c => c.Subcategories).WithOne(s => s.ParentCategory);

        modelBuilder.Entity<Subcategory>().HasKey(s => s.Id);
        modelBuilder.Entity<Subcategory>().Property(s => s.Name).IsRequired();
        modelBuilder.Entity<Subcategory>().HasIndex(s => s.Name).IsUnique();
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
        modelBuilder.Entity<County>().HasIndex(c => c.Name).IsUnique();
        modelBuilder.Entity<County>().HasIndex(c => c.Code).IsUnique();
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
        modelBuilder.Entity<Listing>().Property(l => l.Title).IsRequired();
        modelBuilder.Entity<Listing>().HasOne(l => l.User).WithMany(u => u.Listings);
        modelBuilder.Entity<Listing>().HasOne(l => l.Subcategory).WithMany(s => s.Listings);
        modelBuilder.Entity<Listing>().HasOne(l => l.Location).WithOne(l => l.Listing);
        modelBuilder.Entity<Listing>().HasOne(l => l.Contact).WithOne(l => l.Listing);
        modelBuilder.Entity<Listing>().HasMany(l => l.FormAnswers).WithOne(f => f.Listing);

        modelBuilder.Entity<FormQuestion>().HasKey(q => q.Id);
        modelBuilder.Entity<FormQuestion>().Property(q => q.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();
        modelBuilder.Entity<FormQuestion>().Property(q => q.Label).IsRequired();
        modelBuilder.Entity<FormQuestion>().HasMany(q => q.Answers)
            .WithOne(a => a.Question)
            .HasForeignKey(a => a.FormQuestionId);
        modelBuilder.Entity<FormQuestion>().HasOne(q => q.Subcategory).WithMany(s => s.FormQuestions);

        modelBuilder.Entity<Firm>().HasKey(f => f.Id);
        modelBuilder.Entity<Firm>().Property(f => f.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();
        modelBuilder.Entity<Firm>().Property(f => f.Name).IsRequired();
        modelBuilder.Entity<Firm>().HasIndex(f => f.Name).IsUnique();
        modelBuilder.Entity<Firm>().HasOne(f => f.Location).WithOne(l => l.Firm)
            .HasForeignKey<Firm>(f => f.LocationId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Firm>().HasOne(f => f.Contact).WithOne(c => c.Firm)
            .HasForeignKey<Firm>(f => f.ContactId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Firm>().HasMany(f => f.Subcategories).WithMany(c => c.Firms)
            .UsingEntity<Dictionary<string, object>>("FirmCategory",
                r => r.HasOne<Subcategory>().WithMany().HasForeignKey("CategoryId"),
                l => l.HasOne<Firm>().WithMany().HasForeignKey("FirmId"));
        modelBuilder.Entity<Firm>().HasMany(f => f.Reviews).WithOne(r => r.Firm);
        modelBuilder.Entity<Firm>().HasMany(f => f.Users).WithOne(u => u.Firm);

        modelBuilder.Entity<FirmLocation>().HasKey(f => f.Id);
        modelBuilder.Entity<FirmLocation>().Property(f => f.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();
        modelBuilder.Entity<FirmLocation>().Property(f => f.Address).IsRequired();
        modelBuilder.Entity<FirmLocation>().Property(f => f.PostalCode).IsRequired();
        modelBuilder.Entity<FirmLocation>().HasOne(f => f.City).WithMany(c => c.FirmLocations);
        modelBuilder.Entity<FirmLocation>().HasOne(f => f.County).WithMany(c => c.FirmLocations);
        modelBuilder.Entity<FirmLocation>().HasOne(f => f.Firm).WithOne(f => f.Location);

        modelBuilder.Entity<FirmContact>().HasKey(f => f.Id);
        modelBuilder.Entity<FirmContact>().Property(f => f.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();
        modelBuilder.Entity<FirmContact>().Property(f => f.Email).IsRequired();
        modelBuilder.Entity<FirmContact>().HasIndex(f => f.Email).IsUnique();
        modelBuilder.Entity<FirmContact>().Property(f => f.Phone).IsRequired();
        modelBuilder.Entity<FirmContact>().HasIndex(f => f.Phone).IsUnique();
        modelBuilder.Entity<FirmContact>().Property(f => f.Fax);
        modelBuilder.Entity<FirmContact>().HasIndex(f => f.Fax).IsUnique();
        modelBuilder.Entity<FirmContact>().HasOne(f => f.Firm).WithOne(f => f.Contact);

        modelBuilder.Entity<Review>().HasKey(r => r.Id);
        modelBuilder.Entity<Review>().Property(r => r.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();
        modelBuilder.Entity<Review>().Property(r => r.Content).IsRequired();
        modelBuilder.Entity<Review>().Property(r => r.Rating).IsRequired();
        modelBuilder.Entity<Review>().Property(r => r.Date).IsRequired();
        modelBuilder.Entity<Review>().HasOne(r => r.Firm).WithMany(f => f.Reviews);

        modelBuilder.Entity<UserTypes.UserAccount>().ToTable("AspNetUsers");
        modelBuilder.Entity<UserTypes.UserAccount>().HasKey(u => u.Id);
        modelBuilder.Entity<UserTypes.UserAccount>().Property(u => u.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();
        modelBuilder.Entity<UserTypes.UserAccount>().Property(u => u.UserName).IsRequired();
        modelBuilder.Entity<UserTypes.UserAccount>().Property(u => u.FirstName).IsRequired();
        modelBuilder.Entity<UserTypes.UserAccount>().Property(u => u.LastName).IsRequired();

        modelBuilder.Entity<UserTypes.FirmAccount>().ToTable("FirmAccounts")
            .HasKey(u => u.UserAccountId);
        modelBuilder.Entity<UserTypes.FirmAccount>().HasOne(u => u.Firm).WithMany(f => f.Users)
            .HasForeignKey(u => u.FirmId);
        modelBuilder.Entity<UserTypes.FirmAccount>().HasOne(u => u.UserAccount)
            .WithOne()
            .HasForeignKey<UserTypes.FirmAccount>(u => u.UserAccountId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserTypes.AdminAccount>().ToTable("AdminAccounts");
        modelBuilder.Entity<UserTypes.AdminAccount>()
            .HasOne(u => u.UserAccount)
            .WithOne()
            .HasForeignKey<UserTypes.AdminAccount>(u => u.UserAccountId)
            .OnDelete(DeleteBehavior.Cascade);
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