using Bidro.Config;
using Bidro.DTOs.LocationComponentsDTOs;
using Bidro.Services;
using Bidro.Validation.FluentValidators;
using Bidro.Validation.ValidationObjects;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Bidro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationComponentsController(
    ILocationComponentsService locationComponentsService,
    PgConnectionPool pgConnectionPool) : ControllerBase
{
    [HttpPost("addCounty")]
    [SwaggerOperation(Summary = "Add a new county")]
    public async Task<IResult> AddCounty(PostCountyDTO countyDTO)
    {
        var countyValidator = new CountyValidator(pgConnectionPool);
        var validationResult = await countyValidator.ValidateAsync(new CountyValidityObject(countyDTO));
        if (!validationResult.IsValid) return Results.BadRequest(validationResult.Errors);

        var result = await locationComponentsService.AddCounty(countyDTO);
        return Results.Created($"/api/locationComponents/{result}", result);
    }

    [HttpPost("addCity")]
    [SwaggerOperation(Summary = "Add a new city")]
    public async Task<IResult> AddCity(PostCityDTO city)
    {
        var cityValidator = new CityValidator(pgConnectionPool);
        var validationResult = await cityValidator.ValidateAsync(new CityValidityObject(city));
        if (!validationResult.IsValid) return Results.BadRequest(validationResult.Errors);

        var result = await locationComponentsService.AddCity(city);
        return Results.Created($"/api/locationComponents/{result}", result);
    }

    [HttpGet("getAllCounties")]
    [SwaggerOperation(Summary = "Get all counties")]
    public async Task<IResult> GetAllCounties()
    {
        var result = await locationComponentsService.GetAllCounties();
        return Results.Ok(result);
    }

    [HttpGet("getAllCities")]
    [SwaggerOperation(Summary = "Get all cities")]
    public async Task<IResult> GetAllCities()
    {
        var result = await locationComponentsService.GetAllCities();
        return Results.Ok(result);
    }
}