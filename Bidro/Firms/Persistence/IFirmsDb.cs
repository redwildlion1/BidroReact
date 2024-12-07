using Microsoft.AspNetCore.Mvc;

namespace Bidro.Firms.Persistence;

public interface IFirmsDb
{
    public Task<IActionResult> GetFirmById(Guid firmId);
    Task<IActionResult> GetFirmByName(string firmName);
    Task<IActionResult> CreateFirm(Firm firm);
    Task<IActionResult> UpdateFirm(Firm firm);
    Task<IActionResult> DeleteFirm(Guid firmId);
}