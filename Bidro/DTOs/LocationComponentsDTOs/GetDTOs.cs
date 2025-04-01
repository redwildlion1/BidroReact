namespace Bidro.DTOs.LocationComponentsDTOs;

public class GetDTOs
{
    public class GetCityDTO(Guid cityId, string name, Guid countyId)
    {
    }

    public class GetCountyDTO(Guid countyId, string name, string code)
    {
    }
}