using Bidro.Firms;
using Bidro.Firms.DTOs;
using Bidro.Firms.Persistence;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Bidro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FirmsController(IFirmsDb firmsDb) : ControllerBase
{

    [HttpGet("{firmId}")]
    [SwaggerOperation (Summary = "Get a firm by its ID")]
    public async Task<IActionResult> GetFirmById(Guid firmId)
    {
        return await firmsDb.GetFirmById(firmId);
    }

    [HttpGet("name/{firmName}")]
    [SwaggerOperation (Summary = "Get a firm by its name")]
    public async Task<IActionResult> GetFirmByName(string firmName)
    {
        return await firmsDb.GetFirmByName(firmName);
    }

    [HttpPost("create")]
    [SwaggerOperation (Summary = "Create a new firm")]
    public async Task<IActionResult> CreateFirm(AddFirmDTO firmDTO)
    {
        return await firmsDb.CreateFirm(firmDTO);
    }

    [HttpPut("update")]
    [SwaggerOperation (Summary = "Update a firm")]
    public async Task<IActionResult> UpdateFirm(Firm firm)
    {
        return await firmsDb.UpdateFirm(firm);
    }

    [HttpDelete("{firmId}")]
    [SwaggerOperation (Summary = "Delete a firm")]
    public async Task<IActionResult> DeleteFirm(Guid firmId)
    {
        return await firmsDb.DeleteFirm(firmId);
    }
}