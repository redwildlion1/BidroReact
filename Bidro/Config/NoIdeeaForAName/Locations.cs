using Bidro.Listings;

namespace Bidro.Config.NoIdeeaForAName;

public record County(Guid Id, string Name)
{
    public Guid Id { get; set; } = Id;
    public string Name { get; set; } = Name;
    
    public virtual required ICollection<City> Cities { get; set; }
    public virtual required ICollection<ListingComponents.Location> Locations { get; set; }
};

public record City(Guid Id, string Name)
{
    public Guid Id { get; set; } = Id;
    public string Name { get; set; } = Name;
    
    public Guid CountyId { get; set; }
    
    public virtual required County County { get; set; }
    public virtual required ICollection<ListingComponents.Location> Locations { get; set; }
};