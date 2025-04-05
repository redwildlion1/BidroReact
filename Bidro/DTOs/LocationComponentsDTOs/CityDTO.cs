namespace Bidro.DTOs.LocationComponentsDTOs;

public record CityDTO
{
    public CityDTO(Guid CountyId,
        string Name)
    {
        this.CountyId = CountyId;
        this.Name = Name;
    }


    public Guid CountyId { get; init; }
    public string Name { get; init; }
}