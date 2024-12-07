using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bidro.FrontEndBuildBlocks.Categories;
using Bidro.LocationComponents;
using Bidro.Reviews;
using Bidro.Users;

namespace Bidro.Firms;

public class Firm(string name, string description, string logo, List<Guid> categoryIds)
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
    public string? Website { get; set; }
    public Guid LocationId { get; set; }
    public Guid ContactId { get; set; }
    public List<Guid> CategoryIds { get; set; } = categoryIds;
    public required FirmContact Contact { get; set; } 
    public required FirmLocation Location { get; set; } 
    public List<Category>? Categories { get; set; } 
    public List<Review>? Reviews { get; set; }
    public List<UserTypes.FirmAccount>? Users { get; set; }
    
}

public class FirmLocation(
    string address,
    Guid cityId,
    Guid countyId,
    string postalCode,
    string? latitude,
    string? longitude)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required Guid Id { get; set; }
    [StringLength(50)]
    public string Address { get; set; } = address;
    [StringLength(10)]
    public string PostalCode { get; set; } = postalCode;
    [StringLength(20)]
    public string? Latitude { get; set; } = latitude;
    [StringLength(20)]
    public string? Longitude { get; set; } = longitude;
    public Guid CityId { get; set; } = cityId;
    public required City City { get; set; } 
    public Guid CountyId { get; set; } = countyId;
    public required County County { get; set; } 
    public required Firm Firm { get; set; }
}

public class FirmContact(Guid id, string email, string phone, string? fax)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; } = id;
    [StringLength(50)]
    public string Email { get; set; } = email;
    [StringLength(20)]
    public string Phone { get; set; } = phone;
    [StringLength(20)]
    public string? Fax { get; set; } = fax;
    public required Firm Firm { get; set; }
}