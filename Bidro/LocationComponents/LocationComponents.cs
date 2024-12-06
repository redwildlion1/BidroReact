using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bidro.Listings;

namespace Bidro.Config.LocationComponents;

public record County(Guid Id, string Name, string Code)
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; } = Id;
    public string Name { get; set; } = Name;
    [StringLength(2)]
    public string Code { get; set; } = Code;
    
    public ICollection<City>? Cities { get; set; }
    public ICollection<ListingComponents.Location>? Locations { get; set; }
};

public record City(Guid Id, string Name)
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; } = Id;
    public string Name { get; set; } = Name;
    
    public Guid CountyId { get; set; }
    
    public County? County { get; set; }
    public ICollection<ListingComponents.Location>? Locations { get; set; }
};

