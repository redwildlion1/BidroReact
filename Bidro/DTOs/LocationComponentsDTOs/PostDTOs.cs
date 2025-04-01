namespace Bidro.DTOs.LocationComponentsDTOs;

public class PostDTOs
{
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
}