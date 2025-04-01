using Bidro.EntityObjects;

namespace Bidro.DTOs.LocationComponentsDTOs;

public record CityDTO(
    Guid CountyId,
    string Name)
{
    public City ToCity()
    {
        return new City(CountyId, Name);
    }
}