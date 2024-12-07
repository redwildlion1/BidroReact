using Bidro.Firms;
using Bidro.Firms.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Bidro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FirmsController(IFirmsDb firmsDb) : ControllerBase
{

    [HttpGet("{firmId}")]
    public async Task<IActionResult> GetFirmById(Guid firmId)
    {
        return await firmsDb.GetFirmById(firmId);
    }

    [HttpGet("name/{firmName}")]
    public async Task<IActionResult> GetFirmByName(string firmName)
    {
        return await firmsDb.GetFirmByName(firmName);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateFirm(Firm firm)
    {
        return await firmsDb.CreateFirm(firm);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateFirm(Firm firm)
    {
        return await firmsDb.UpdateFirm(firm);
    }

    [HttpDelete("{firmId}")]
    public async Task<IActionResult> DeleteFirm(Guid firmId)
    {
        return await firmsDb.DeleteFirm(firmId);
    }
}