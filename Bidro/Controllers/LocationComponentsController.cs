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
    public async Task<IActionResult> AddCounty(PostDTOs.PostCountyDTO countyDTO)
    {
        var result = await locationComponentsService.AddCounty(countyDTO);
        return CreatedAtAction(nameof(AddCounty), new { countyId = result }, result);
    }

    [HttpPost("addCity")]
    [SwaggerOperation(Summary = "Add a new city")]
    public async Task<IActionResult> AddCity(PostDTOs.PostCityDTO city)
    {
        var result = await locationComponentsService.AddCity(city);
        return CreatedAtAction(nameof(AddCity), new { cityId = result }, result);
    }

    [HttpGet("getAllCounties")]
    [SwaggerOperation(Summary = "Get all counties")]
    public async Task<IActionResult> GetAllCounties()
    {
        var result = await locationComponentsService.GetAllCounties();
        return Ok(result);
    }

    [HttpGet("getAllCities")]
    [SwaggerOperation(Summary = "Get all cities")]
    public async Task<IActionResult> GetAllCities()
    {
        var result = await locationComponentsService.GetAllCities();
        return Ok(result);
    }
}