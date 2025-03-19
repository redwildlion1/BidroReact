using Bidro.DTOs.LocationComponentsDTOs;
using Bidro.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Bidro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationComponentsController(ILocationComponentsService locationComponentsService) : ControllerBase
{
    [HttpPost("addCounty")]
    [SwaggerOperation(Summary = "Add a new county")]
    public async Task<IActionResult> AddCounty(CountyDTO countyDTO)
    {
        var county = countyDTO.ToCounty();
        return await locationComponentsService.AddCounty(county);
    }

    [HttpPost("addCity")]
    [SwaggerOperation(Summary = "Add a new city")]
    public async Task<IActionResult> AddCity(CityDTO cityDTO)
    {
        var city = cityDTO.ToCity();
        return await locationComponentsService.AddCity(city);
    }

    [HttpGet("getAllCounties")]
    [SwaggerOperation(Summary = "Get all counties")]
    public async Task<IActionResult> GetAllCounties()
    {
        return await locationComponentsService.GetAllCounties();
    }

    [HttpGet("getAllCities")]
    [SwaggerOperation(Summary = "Get all cities")]
    public async Task<IActionResult> GetAllCities()
    {
        return await locationComponentsService.GetAllCities();
    }
}