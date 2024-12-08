namespace Bidro.LocationComponents.DTOs;

public record CountyDTO(
    string Name,
    string Code)
{
    public County ToCounty()
    {
        return new County(Name, Code);
    }
}