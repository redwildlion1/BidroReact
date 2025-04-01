using Bidro.DTOs.LocationComponentsDTOs;

namespace Bidro.Services;

public interface ILocationComponentsService
{
    public Task<GetDTOs.GetCountyDTO> AddCounty(PostDTOs.PostCountyDTO postCounty);
    public Task<GetDTOs.GetCityDTO> AddCity(PostDTOs.PostCityDTO postCity);
    public Task<IEnumerable<GetDTOs.GetCountyDTO>> GetAllCounties();
    public Task<IEnumerable<GetDTOs.GetCityDTO>> GetAllCities();
}