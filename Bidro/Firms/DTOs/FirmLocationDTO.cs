namespace Bidro.Firms.DTOs;

public record FirmLocationDTO(string Address, Guid CityId, Guid CountyId, string? PostalCode,
    string? Latitude, string? Longitude)
{
    public FirmLocation ToFirmLocation()
    {
        return new FirmLocation(Address, CityId, CountyId,
            PostalCode, Latitude, Longitude);
    }
}