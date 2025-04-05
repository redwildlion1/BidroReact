using Bidro.DTOs.LocationComponentsDTOs;

namespace Bidro.Services;

public interface ILocationComponentsService
{
    public Task<GetCountyDTO> AddCounty(PostCountyDTO postCounty);
    public Task<GetCityDTO> AddCity(PostCityDTO postCity);
    public Task<IEnumerable<GetCountyDTO>> GetAllCounties();
    public Task<IEnumerable<GetCityDTO>> GetAllCities();
}