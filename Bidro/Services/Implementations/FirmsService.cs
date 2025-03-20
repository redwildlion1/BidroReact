using Bidro.Config;
using Bidro.DTOs.FirmDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bidro.Services.Implementations;

public class FirmsService(EntityDbContext dbContext) : IFirmsService
{
    public async Task<IActionResult> GetFirmById(Guid firmId)
    {
        var firm = await dbContext.Firms
            .Include(f => f.Contact)
            .Include(f => f.Location)
            .Include(f => f.Subcategories)
            .Include(f => f.Reviews)
            .FirstOrDefaultAsync(f => f.Id == firmId);

        if (firm == null) return new NotFoundResult();
        return new OkObjectResult(firm);
    }

    public async Task<IActionResult> GetFirmsInCategory(Guid categoryId)
    {
        var firms = await dbContext.Firms
            .Include(f => f.Contact)
            .Include(f => f.Location)
            .Where(f => f.Subcategories!.Any(s => s.ParentCategoryId == categoryId))
            .ToListAsync();
        if (firms.Count == 0) return new NotFoundResult();
        return new OkObjectResult(firms);
    }

    public async Task<IActionResult> GetFirmsInSubcategory(Guid subcategoryId)
    {
        var firms = await dbContext.Firms
            .Include(f => f.Contact)
            .Include(f => f.Location)
            .Where(f => f.Subcategories!.Any(s => s.Id == subcategoryId))
            .ToListAsync();
        if (firms.Count == 0) return new NotFoundResult();
        return new OkObjectResult(firms);
    }

    public async Task<IActionResult> PostFirm(PostDTOs.PostFirmDTO postFirmDTO)
    {
        await dbContext.Firms.AddAsync(postFirmDTO.ToFirm());
        await dbContext.SaveChangesAsync();
        return new OkObjectResult(postFirmDTO.Base.Name);
    }

    public async Task<IActionResult> UpdateFirmName(UpdateDTOs.UpdateFirmNameDTO updateFirmNameDTO)
    {
        var firm = await dbContext.Firms.FirstOrDefaultAsync(f => f.Id == updateFirmNameDTO.FirmId);
        if (firm == null) return new NotFoundResult();
        firm.Name = updateFirmNameDTO.Name;
        await dbContext.SaveChangesAsync();
        return new OkResult();
    }

    public async Task<IActionResult> UpdateFirmDescription(UpdateDTOs.UpdateFirmDescriptionDTO updateFirmDescriptionDTO)
    {
        var firm = await dbContext.Firms.FirstOrDefaultAsync(f => f.Id == updateFirmDescriptionDTO.FirmId);
        if (firm == null) return new NotFoundResult();
        firm.Description = updateFirmDescriptionDTO.Description;
        await dbContext.SaveChangesAsync();
        return new OkResult();
    }

    public async Task<IActionResult> UpdateFirmLogo(UpdateDTOs.UpdateFirmLogoDTO updateFirmLogoDTO)
    {
        var firm = await dbContext.Firms.FirstOrDefaultAsync(f => f.Id == updateFirmLogoDTO.FirmId);
        if (firm == null) return new NotFoundResult();
        firm.Logo = updateFirmLogoDTO.Logo;
        await dbContext.SaveChangesAsync();
        return new OkResult();
    }

    public async Task<IActionResult> UpdateFirmWebsite(UpdateDTOs.UpdateFirmWebsiteDTO updateFirmWebsiteDTO)
    {
        var firm = await dbContext.Firms.FirstOrDefaultAsync(f => f.Id == updateFirmWebsiteDTO.FirmId);
        if (firm == null) return new NotFoundResult();
        firm.Website = updateFirmWebsiteDTO.Website;
        await dbContext.SaveChangesAsync();
        return new OkResult();
    }

    public async Task<IActionResult> UpdateFirmLocation(UpdateDTOs.UpdateFirmLocationDTO updateFirmLocationDTO)
    {
        var firm = await dbContext.Firms.Include(firm => firm.Location)
            .FirstOrDefaultAsync(f => f.Id == updateFirmLocationDTO.Id);
        if (firm == null) return new NotFoundResult();
        firm.Location!.CityId = updateFirmLocationDTO.CityId;
        firm.Location.CountyId = updateFirmLocationDTO.CountyId;
        firm.Location.Address = updateFirmLocationDTO.Address;
        firm.Location.PostalCode = updateFirmLocationDTO.PostalCode;
        firm.Location.Latitude = updateFirmLocationDTO.Latitude;
        firm.Location.Longitude = updateFirmLocationDTO.Longitude;
        await dbContext.SaveChangesAsync();
        return new OkResult();
    }

    public async Task<IActionResult> UpdateFirmContact(UpdateDTOs.UpdateFirmContactDTO updateFirmContactDTO)
    {
        var firm = await dbContext.Firms.Include(firm => firm.Contact)
            .FirstOrDefaultAsync(f => f.Id == updateFirmContactDTO.Id);
        if (firm == null) return new NotFoundResult();
        firm.Contact!.Email = updateFirmContactDTO.Email;
        firm.Contact.Phone = updateFirmContactDTO.Phone;
        firm.Contact.Fax = updateFirmContactDTO.Fax;
        await dbContext.SaveChangesAsync();
        return new OkResult();
    }

    public async Task<IActionResult> SuspendFirm(Guid firmId)
    {
        var firm = await dbContext.Firms.FirstOrDefaultAsync(f => f.Id == firmId);
        if (firm == null) return new NotFoundResult();
        firm.IsSuspended = true;
        await dbContext.SaveChangesAsync();
        return new OkResult();
    }

    public async Task<IActionResult> UnsuspendFirm(Guid firmId)
    {
        var firm = await dbContext.Firms.FirstOrDefaultAsync(f => f.Id == firmId);
        if (firm == null) return new NotFoundResult();
        firm.IsSuspended = false;
        await dbContext.SaveChangesAsync();
        return new OkResult();
    }

    public async Task<IActionResult> VerifyFirm(Guid firmId)
    {
        var firm = await dbContext.Firms.FirstOrDefaultAsync(f => f.Id == firmId);
        if (firm == null) return new NotFoundResult();
        firm.IsVerified = true;
        await dbContext.SaveChangesAsync();
        return new OkResult();
    }
}