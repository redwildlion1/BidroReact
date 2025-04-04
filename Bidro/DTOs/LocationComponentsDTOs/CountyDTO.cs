using Bidro.EntityObjects;

namespace Bidro.DTOs.LocationComponentsDTOs;

public record CountyDTO(
    string Name,
    string Code)
{
    public County ToCounty()
    {
        return new County(Name, Code);
    }
}