using Bidro.Config.LocationComponents;
using Bidro.LocationComponents.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Bidro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationComponentsController(ILocationComponentsDb locationComponentsDb) : ControllerBase
{
    [HttpPost ("addCounty")]
    public async Task<IActionResult> AddCounty(County county)
    {
        return await locationComponentsDb.AddCounty(county);
    }
    
    [HttpPost ("addCity")]
    public async Task<IActionResult> AddCity(City city)
    {
        return await locationComponentsDb.AddCity(city);
    }
}