using Bidro.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bidro.Firms.Persistence;

public class FirmsDb(DbContextOptions<EntityDbContext> options) : IFirmsDb
{
    public async Task<IActionResult> GetFirmById(Guid firmId)
    {
        await using var db = new EntityDbContext(options);
        var firm = await db.Firms
            .Include(f => f.Contact)
            .Include(f => f.Location)
            .Include(f => f.Categories)
            .Include(f => f.Reviews)
            .FirstOrDefaultAsync(f => f.Id == firmId);
        
        if (firm == null) return new NotFoundResult();
        return new OkObjectResult(firm);
    }

    public async Task<IActionResult> GetFirmByName(string firmName)
    {
        await using var db = new EntityDbContext(options);
        var firm = await db.Firms
            .Include(f => f.Contact)
            .Include(f => f.Location)
            .Include(f => f.Categories)
            .Include(f => f.Reviews)
            .FirstOrDefaultAsync(f => f.Name == firmName);
        
        if (firm == null) return new NotFoundResult();
        return new OkObjectResult(firm);
    }

    public async Task<IActionResult> CreateFirm(Firm firm)
    {
        await using var db = new EntityDbContext(options);
       
        // Add related entities
        await db.FirmLocations.AddAsync(firm.Location);
        await db.FirmContacts.AddAsync(firm.Contact);
        
        // Add firm
        await db.Firms.AddAsync(firm);
        await db.SaveChangesAsync();
        return new OkResult();
    }

    public async Task<IActionResult> UpdateFirm(Firm firm)
    {
        await using var db = new EntityDbContext(options);
        db.Firms.Update(firm);
        await db.SaveChangesAsync();
        return new OkResult();
    }

    public async Task<IActionResult> DeleteFirm(Guid firmId)
    {
        await using var db = new EntityDbContext(options);
        var firm = await db.Firms.FirstOrDefaultAsync(f => f.Id == firmId);
        if (firm != null) db.Firms.Remove(firm);
        else return new NotFoundResult();
        await db.SaveChangesAsync();
        return new OkResult();
    }
}