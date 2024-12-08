using Bidro.Config;
using Bidro.Firms.DTOs;
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

    public async Task<IActionResult> CreateFirm(AddFirmDTO firmDTO)
    {
        await using var db = new EntityDbContext(options);

        // Add related entities
        var location = firmDTO.Location.ToFirmLocation();
        var contact = firmDTO.Contact.ToFirmContact();

        await db.FirmLocations.AddAsync(location);
        await db.FirmContacts.AddAsync(contact);

        // Create firm entity
        var firm = new Firm(
            firmDTO.Name, firmDTO.Description, firmDTO.Logo,
            firmDTO.Website, firmDTO.CategoryIds, contact.Id,
            location.Id
        );
        
        // Add firm
        await db.Firms.AddAsync(firm);

        // Save all changes in one transaction
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