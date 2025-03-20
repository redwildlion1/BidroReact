using Bidro.DTOs.FirmDTOs;
using Microsoft.AspNetCore.Mvc;

namespace Bidro.Services;

public interface IFirmsService
{
    public Task<IActionResult> GetFirmById(Guid firmId);
    public Task<IActionResult> GetFirmsInCategory(Guid categoryId);

    public Task<IActionResult> GetFirmsInSubcategory(Guid subcategoryId);

    /*public Task<IActionResult> GetFirmsBySortCriteria(string sortCriteria);*/
    public Task<IActionResult> PostFirm(PostDTOs.PostFirmDTO postFirmDTO);
    public Task<IActionResult> UpdateFirmName(UpdateDTOs.UpdateFirmNameDTO updateFirmNameDTO);
    public Task<IActionResult> UpdateFirmDescription(UpdateDTOs.UpdateFirmDescriptionDTO updateFirmDescriptionDTO);
    public Task<IActionResult> UpdateFirmLogo(UpdateDTOs.UpdateFirmLogoDTO updateFirmLogoDTO);
    public Task<IActionResult> UpdateFirmWebsite(UpdateDTOs.UpdateFirmWebsiteDTO updateFirmWebsiteDTO);
    public Task<IActionResult> UpdateFirmLocation(UpdateDTOs.UpdateFirmLocationDTO updateFirmLocationDTO);
    public Task<IActionResult> UpdateFirmContact(UpdateDTOs.UpdateFirmContactDTO updateFirmContactDTO);
    public Task<IActionResult> SuspendFirm(Guid firmId);
    public Task<IActionResult> UnsuspendFirm(Guid firmId);
    public Task<IActionResult> VerifyFirm(Guid firmId);
}