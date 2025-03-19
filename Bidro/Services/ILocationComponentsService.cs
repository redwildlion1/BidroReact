using Bidro.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace Bidro.Services;

public interface ILocationComponentsService
{
    public Task<IActionResult> AddCounty(County county);
    public Task<IActionResult> AddCity(City city);
    public Task<IActionResult> GetAllCounties();
    public Task<IActionResult> GetAllCities();
}