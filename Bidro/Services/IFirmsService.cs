using Bidro.DTOs.FirmDTOs;

namespace Bidro.Services;

public interface IFirmsService
{
    public Task<GetDTOs.GetFirmDTO> GetFirmById(Guid firmId);
    public Task<IEnumerable<GetDTOs.GetFirmDTO>> GetFirmsInCategory(Guid categoryId);

    public Task<IEnumerable<GetDTOs.GetFirmDTO>> GetFirmsInSubcategory(Guid subcategoryId);

    /*public Task<IActionResult> GetFirmsBySortCriteria(string sortCriteria);*/
    public Task<Guid> PostFirm(PostDTOs.PostFirmDTO postFirmDTO);
    public Task<bool> UpdateFirmName(UpdateDTOs.UpdateFirmNameDTO updateFirmNameDTO);
    public Task<bool> UpdateFirmDescription(UpdateDTOs.UpdateFirmDescriptionDTO updateFirmDescriptionDTO);
    public Task<bool> UpdateFirmLogo(UpdateDTOs.UpdateFirmLogoDTO updateFirmLogoDTO);
    public Task<bool> UpdateFirmWebsite(UpdateDTOs.UpdateFirmWebsiteDTO updateFirmWebsiteDTO);
    public Task<bool> UpdateFirmLocation(UpdateDTOs.UpdateFirmLocationDTO updateFirmLocationDTO);
    public Task<bool> UpdateFirmContact(UpdateDTOs.UpdateFirmContactDTO updateFirmContactDTO);
    public Task<bool> SuspendFirm(Guid firmId);
    public Task<bool> UnsuspendFirm(Guid firmId);
    public Task<bool> VerifyFirm(Guid firmId);
}