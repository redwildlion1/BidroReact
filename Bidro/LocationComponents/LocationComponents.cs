using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bidro.Firms;
using Bidro.Listings;

namespace Bidro.LocationComponents;

public record County(string Name, string Code)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string Name { get; set; } = Name;
    [StringLength(2)]
    public string Code { get; set; } = Code;
    public ICollection<City>? Cities { get; set; }
    public ICollection<ListingComponents.Location>? Locations { get; set; }
    public ICollection<FirmLocation>? FirmLocations { get; set; }
};

public record City(Guid CountyId, string Name)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string Name { get; set; } = Name;
    public Guid CountyId { get; set; } = CountyId;
    public County? County { get; set; }
    public ICollection<ListingComponents.Location>? Locations { get; set; }
    public ICollection<FirmLocation>? FirmLocations { get; set; }
};

