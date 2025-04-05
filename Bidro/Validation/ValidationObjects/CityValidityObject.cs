using Bidro.DTOs.LocationComponentsDTOs;

namespace Bidro.Validation.ValidationObjects;

public class CityValidityObject(PostCityDTO cityDTO)
{
    public string Name { get; } = cityDTO.Name;
    public Guid CountyId { get; } = cityDTO.CountyId;
}