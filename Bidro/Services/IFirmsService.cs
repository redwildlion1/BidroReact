using Bidro.DTOs.FirmDTOs;

namespace Bidro.Services;

public interface IFirmsService
{
    public Task<GetFirmDTO> GetFirmById(Guid firmId);
    public Task<IEnumerable<GetFirmDTO>> GetFirmsInCategory(Guid categoryId);

    public Task<IEnumerable<GetFirmDTO>> GetFirmsInSubcategory(Guid subcategoryId);

    /*public Task<IActionResult> GetFirmsBySortCriteria(string sortCriteria);*/
    public Task<Guid> PostFirm(PostFirmDTO postFirmDTO);
    public Task<bool> UpdateFirmName(UpdateFirmNameDTO updateFirmNameDTO);
    public Task<bool> UpdateFirmDescription(UpdateFirmDescriptionDTO updateFirmDescriptionDTO);
    public Task<bool> UpdateFirmLogo(UpdateFirmLogoDTO updateFirmLogoDTO);
    public Task<bool> UpdateFirmWebsite(UpdateFirmWebsiteDTO updateFirmWebsiteDTO);
    public Task<bool> UpdateFirmLocation(UpdateFirmLocationDTO updateFirmLocationDTO);
    public Task<bool> UpdateFirmContact(UpdateFirmContactDTO updateFirmContactDTO);
    public Task<bool> SuspendFirm(Guid firmId);
    public Task<bool> UnsuspendFirm(Guid firmId);
    public Task<bool> VerifyFirm(Guid firmId);
}