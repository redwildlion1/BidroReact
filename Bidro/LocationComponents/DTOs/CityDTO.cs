namespace Bidro.LocationComponents.DTOs;

public record CityDTO(
    Guid CountyId,
    string Name)
{
    public City ToCity()
    {
        return new City(CountyId, Name);
    }
}