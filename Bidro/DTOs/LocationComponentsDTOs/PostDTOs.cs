namespace Bidro.DTOs.LocationComponentsDTOs;

public record PostCityDTO(
    Guid CountyId,
    string Name)
{
}

public record PostCountyDTO(
    string Name,
    string Code)
{
}