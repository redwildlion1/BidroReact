namespace Bidro.DTOs.LocationComponentsDTOs;

public class GetCityDTO
{
    public GetCityDTO(Guid cityId, string name, Guid countyId)
    {
        CityId = cityId;
        Name = name;
        CountyId = countyId;
    }

    public Guid CityId { get; }
    public string Name { get; }
    public Guid CountyId { get; }
}

public class GetCountyDTO
{
    public GetCountyDTO(Guid countyId, string name, string code)
    {
        CountyId = countyId;
        Name = name;
        Code = code;
    }

    public Guid CountyId { get; }
    public string Name { get; }
    public string Code { get; }
}