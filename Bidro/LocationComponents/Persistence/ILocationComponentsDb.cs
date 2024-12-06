using Bidro.Config.LocationComponents;
using Microsoft.AspNetCore.Mvc;

namespace Bidro.LocationComponents.Persistence;

public interface ILocationComponentsDb
{
    public Task<IActionResult> AddCounty(County county);
    public Task<IActionResult> AddCity(City city);
}