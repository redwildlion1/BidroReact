using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bidro.FrontEndBuildBlocks.Categories;
using Bidro.LocationComponents;
using Bidro.Reviews;
using Bidro.Users;

namespace Bidro.Firms;

public class Firm(
    string name, string description,
    string logo, string? website,
    List<string>? categoryIds,
    Guid contactId, Guid locationId)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    [StringLength(50)]
    public string Name { get; set; } = name;
    [StringLength(500)]
    public string Description { get; set; } = description;
    [StringLength(100)]
    
    public string? Logo { get; set; } = logo;
    [StringLength(100)]
    public string? Website { get; set; } = website;

    public Guid LocationId { get; set; } = locationId;
    public Guid ContactId { get; set; } = contactId;
    public List<string>? CategoryIds { get; set; } = categoryIds;
    public FirmContact? Contact { get; set; }
    public FirmLocation? Location { get; set; }
    public List<Category>? Categories { get; set; } 
    public List<Review>? Reviews { get; set; }
    public List<UserTypes.FirmAccount>? Users { get; set; }
    
}

public class FirmLocation(
    string address,
    Guid cityId,
    Guid countyId,
    string? postalCode,
    string? latitude,
    string? longitude)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    [StringLength(50)]
    public string Address { get; set; } = address;
    [StringLength(10)]
    public string? PostalCode { get; set; } = postalCode;
    [StringLength(20)]
    public string? Latitude { get; set; } = latitude;
    [StringLength(20)]
    public string? Longitude { get; set; } = longitude;
    public Guid CityId { get; set; } = cityId;
    public City? City { get; set; } 
    public Guid CountyId { get; set; } = countyId;
    public County? County { get; set; } 
    public Firm? Firm { get; set; }
}

public class FirmContact(string email, string phone, string? fax)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    [StringLength(50)]
    public string Email { get; set; } = email;
    [StringLength(20)]
    public string Phone { get; set; } = phone;
    [StringLength(20)]
    public string? Fax { get; set; } = fax;
    public Firm? Firm { get; set; }
}