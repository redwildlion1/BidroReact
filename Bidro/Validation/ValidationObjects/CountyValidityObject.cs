using Bidro.DTOs.LocationComponentsDTOs;

namespace Bidro.Validation.ValidationObjects;

public class CountyValidityObject(PostCountyDTO countyDTO)
{
    public string Code { get; } = countyDTO.Code;
    public string Name { get; } = countyDTO.Name;
}