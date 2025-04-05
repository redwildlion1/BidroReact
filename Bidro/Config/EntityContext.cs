using Bidro.EntityObjects;
using Microsoft.EntityFrameworkCore;

namespace Bidro.Config;

#pragma warning disable IL2026
#pragma warning disable IL3050
public class EntityDbContext(DbContextOptions<EntityDbContext> options) : DbContext(options)
#pragma warning restore IL3050
#pragma warning restore IL2026
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Subcategory> Subcategories { get; set; }
    public DbSet<County> Counties { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<ListingLocation> ListingLocations { get; set; }
    public DbSet<ListingContact> ListingContacts { get; set; }
    public DbSet<FormAnswer> FormAnswers { get; set; }
    public DbSet<Listing> Listings { get; set; }
    public DbSet<FormQuestion> FormQuestions { get; set; }
    public DbSet<Firm> Firms { get; set; }
    public DbSet<FirmLocation> FirmLocations { get; set; }
    public DbSet<FirmContact> FirmContacts { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Ensure the uuid-ossp extension is created
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<Category>().HasKey(c => c.Id);
        modelBuilder.Entity<Category>().Property(c => c.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("uuid_generate_v4()")
            .IsRequired();
        modelBuilder.Entity<Category>().Property(c => c.Name)
            .IsRequired();
        modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
        modelBuilder.Entity<Category>().Property(c => c.Icon).IsRequired();
        modelBuilder.Entity<Category>().HasMany(c => c.Subcategories).WithOne(s => s.ParentCategory);

        modelBuilder.Entity<Subcategory>().HasKey(s => s.Id);
        modelBuilder.Entity<Subcategory>().Property(s => s.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("uuid_generate_v4()")
            .IsRequired();
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
            .HasDefaultValueSql("uuid_generate_v4()")
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
            .HasDefaultValueSql("uuid_generate_v4()")
            .IsRequired();
        modelBuilder.Entity<City>().Property(c => c.Name).IsRequired();
        modelBuilder.Entity<City>().HasOne(c => c.County).WithMany(c => c.Cities);
        modelBuilder.Entity<City>().HasMany(c => c.Locations).WithOne(c => c.City);

        modelBuilder.Entity<ListingLocation>().HasKey(l => l.ListingId);
        modelBuilder.Entity<ListingLocation>().Property(l => l.ListingId)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("uuid_generate_v4()")
            .IsRequired();
        modelBuilder.Entity<ListingLocation>().HasOne(l => l.Listing).WithOne(l => l.Location);
        modelBuilder.Entity<ListingLocation>().HasOne(l => l.County).WithMany(c => c.Locations);
        modelBuilder.Entity<ListingLocation>().HasOne(l => l.City).WithMany(c => c.Locations);

        modelBuilder.Entity<ListingContact>().HasKey(c => c.ListingId);
        modelBuilder.Entity<ListingContact>().Property(c => c.ListingId)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("uuid_generate_v4()")
            .IsRequired();
        modelBuilder.Entity<ListingContact>().HasOne(c => c.Listing).WithOne(l => l.Contact);

        modelBuilder.Entity<FormAnswer>().HasKey(f => f.Id);
        modelBuilder.Entity<FormAnswer>().Property(f => f.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("uuid_generate_v4()")
            .IsRequired();
        modelBuilder.Entity<FormAnswer>()
            .HasOne(f => f.Listing)
            .WithMany(l => l.FormAnswers);
        modelBuilder.Entity<FormAnswer>()
            .HasOne(f => f.Question)
            .WithMany(q => q.Answers);

        modelBuilder.Entity<Listing>().HasKey(l => l.Id);
        modelBuilder.Entity<Listing>().Property(l => l.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("uuid_generate_v4()")
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
            .HasDefaultValueSql("uuid_generate_v4()")
            .IsRequired();
        modelBuilder.Entity<FormQuestion>().Property(q => q.Label).IsRequired();
        modelBuilder.Entity<FormQuestion>().HasMany(q => q.Answers)
            .WithOne(a => a.Question)
            .HasForeignKey(a => a.FormQuestionId);
        modelBuilder.Entity<FormQuestion>().HasOne(q => q.Subcategory).WithMany(s => s.FormQuestions);

        modelBuilder.Entity<Firm>().HasKey(f => f.Id);
        modelBuilder.Entity<Firm>().Property(f => f.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("uuid_generate_v4()")
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
            .UsingEntity<Dictionary<string, object>>("FirmSubcategories",
                r => r.HasOne<Subcategory>().WithMany().HasForeignKey("SubcategoryId"),
                l => l.HasOne<Firm>().WithMany().HasForeignKey("FirmId"));
        modelBuilder.Entity<Firm>().HasMany(f => f.Reviews).WithOne(r => r.Firm);
        modelBuilder.Entity<Firm>().Property(f => f.Rating).HasDefaultValue(0);
        modelBuilder.Entity<Firm>().Property(f => f.ReviewCount).HasDefaultValue(0);
        modelBuilder.Entity<Firm>().Property(f => f.IsVerified).HasDefaultValue(false);
        modelBuilder.Entity<Firm>().Property(f => f.IsSuspended).HasDefaultValue(false);
        modelBuilder.Entity<Firm>()
            .HasMany(f => f.Users)
            .WithMany(u => u.Firms)
            .UsingEntity<Dictionary<string, object>>(
                "FirmUsers",
                j => j.HasOne<User>().WithMany().HasForeignKey("UserId"),
                j => j.HasOne<Firm>().WithMany().HasForeignKey("FirmId")
            );

        modelBuilder.Entity<FirmLocation>().HasKey(f => f.Id);
        modelBuilder.Entity<FirmLocation>().Property(f => f.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("uuid_generate_v4()")
            .IsRequired();
        modelBuilder.Entity<FirmLocation>().Property(f => f.Address).IsRequired();
        modelBuilder.Entity<FirmLocation>().Property(f => f.PostalCode).IsRequired();
        modelBuilder.Entity<FirmLocation>().HasOne(f => f.City).WithMany(c => c.FirmLocations);
        modelBuilder.Entity<FirmLocation>().HasOne(f => f.County).WithMany(c => c.FirmLocations);
        modelBuilder.Entity<FirmLocation>().HasOne(f => f.Firm).WithOne(f => f.Location);

        modelBuilder.Entity<FirmContact>().HasKey(f => f.Id);
        modelBuilder.Entity<FirmContact>().Property(f => f.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("uuid_generate_v4()")
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
            .HasDefaultValueSql("uuid_generate_v4()")
            .IsRequired();
        modelBuilder.Entity<Review>().Property(r => r.ReviewText).IsRequired();
        modelBuilder.Entity<Review>().Property(r => r.Rating).IsRequired();
        modelBuilder.Entity<Review>().Property(r => r.Date).IsRequired();
        modelBuilder.Entity<Review>().HasOne(r => r.Firm).WithMany(f => f.Reviews);

        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);
        modelBuilder.Entity<User>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("uuid_generate_v4()")
            .IsRequired();
        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .IsRequired();
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
        modelBuilder.Entity<User>()
            .Property(u => u.PasswordHash)
            .IsRequired();
        modelBuilder.Entity<User>()
            .Property(u => u.FirstName)
            .IsRequired();
        modelBuilder.Entity<User>()
            .Property(u => u.LastName)
            .IsRequired();
        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .IsRequired();
        modelBuilder.Entity<User>()
            .Property(u => u.SecurityStamp)
            .IsRequired();
        modelBuilder.Entity<User>()
            .Property(u => u.EmailConfirmed)
            .IsRequired();
        modelBuilder.Entity<User>()
            .Property(u => u.PhoneNumber)
            .IsRequired();
        modelBuilder.Entity<User>()
            .Property(u => u.PhoneNumberConfirmed)
            .IsRequired();
        modelBuilder.Entity<User>()
            .Property(u => u.TwoFactorEnabled)
            .IsRequired();
        modelBuilder.Entity<User>()
            .Property(u => u.LockoutEnabled)
            .IsRequired();
        modelBuilder.Entity<User>()
            .Property(u => u.AccessFailedCount)
            .IsRequired();
        // now the default values
        modelBuilder.Entity<User>()
            .Property(u => u.EmailConfirmed)
            .HasDefaultValue(false);
        modelBuilder.Entity<User>()
            .Property(u => u.PhoneNumberConfirmed)
            .HasDefaultValue(false);
        modelBuilder.Entity<User>()
            .Property(u => u.TwoFactorEnabled)
            .HasDefaultValue(false);
        modelBuilder.Entity<User>()
            .Property(u => u.LockoutEnabled)
            .HasDefaultValue(false);
        modelBuilder.Entity<User>()
            .Property(u => u.LockoutEnd)
            .HasDefaultValue(null);
        modelBuilder.Entity<User>()
            .Property(u => u.AccessFailedCount)
            .HasDefaultValue(0);
    }
}