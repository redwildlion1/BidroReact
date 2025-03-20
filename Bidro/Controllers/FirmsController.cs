using Bidro.DTOs.FirmDTOs;
using Bidro.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Bidro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FirmsController(IFirmsService firmsService) : ControllerBase
{
    [HttpGet("{firmId}")]
    [SwaggerOperation(Summary = "Get a firm by its ID")]
    public async Task<IActionResult> GetFirmById(Guid firmId)
    {
        return await firmsService.GetFirmById(firmId);
    }

    [HttpGet("category/{categoryId}")]
    [SwaggerOperation(Summary = "Get firms in a category")]
    public async Task<IActionResult> GetFirmsInCategory(Guid categoryId)
    {
        return await firmsService.GetFirmsInCategory(categoryId);
    }

    [HttpGet("subcategory/{subcategoryId}")]
    [SwaggerOperation(Summary = "Get firms in a subcategory")]
    public async Task<IActionResult> GetFirmsInSubcategory(Guid subcategoryId)
    {
        return await firmsService.GetFirmsInSubcategory(subcategoryId);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Post a new firm")]
    public async Task<IActionResult> PostFirm(PostDTOs.PostFirmDTO postFirmDTO)
    {
        return await firmsService.PostFirm(postFirmDTO);
    }

    [HttpPut("updateName")]
    [SwaggerOperation(Summary = "Update firm name")]
    public async Task<IActionResult> UpdateFirmName(UpdateDTOs.UpdateFirmNameDTO updateFirmNameDTO)
    {
        return await firmsService.UpdateFirmName(updateFirmNameDTO);
    }

    [HttpPut("updateDescription")]
    [SwaggerOperation(Summary = "Update firm description")]
    public async Task<IActionResult> UpdateFirmDescription(UpdateDTOs.UpdateFirmDescriptionDTO updateFirmDescriptionDTO)
    {
        return await firmsService.UpdateFirmDescription(updateFirmDescriptionDTO);
    }

    [HttpPut("updateLogo")]
    [SwaggerOperation(Summary = "Update firm logo")]
    public async Task<IActionResult> UpdateFirmLogo(UpdateDTOs.UpdateFirmLogoDTO updateFirmLogoDTO)
    {
        return await firmsService.UpdateFirmLogo(updateFirmLogoDTO);
    }

    [HttpPut("updateWebsite")]
    [SwaggerOperation(Summary = "Update firm website")]
    public async Task<IActionResult> UpdateFirmWebsite(UpdateDTOs.UpdateFirmWebsiteDTO updateFirmWebsiteDTO)
    {
        return await firmsService.UpdateFirmWebsite(updateFirmWebsiteDTO);
    }

    [HttpPut("updateLocation")]
    [SwaggerOperation(Summary = "Update firm location")]
    public async Task<IActionResult> UpdateFirmLocation(UpdateDTOs.UpdateFirmLocationDTO updateFirmLocationDTO)
    {
        return await firmsService.UpdateFirmLocation(updateFirmLocationDTO);
    }

    [HttpPut("updateContact")]
    [SwaggerOperation(Summary = "Update firm contact")]
    public async Task<IActionResult> UpdateFirmContact(UpdateDTOs.UpdateFirmContactDTO updateFirmContactDTO)
    {
        return await firmsService.UpdateFirmContact(updateFirmContactDTO);
    }

    [HttpPut("suspend/{firmId}")]
    [SwaggerOperation(Summary = "Suspend a firm")]
    public async Task<IActionResult> SuspendFirm(Guid firmId)
    {
        return await firmsService.SuspendFirm(firmId);
    }

    [HttpPut("unsuspend/{firmId}")]
    [SwaggerOperation(Summary = "Unsuspend a firm")]
    public async Task<IActionResult> UnsuspendFirm(Guid firmId)
    {
        return await firmsService.UnsuspendFirm(firmId);
    }

    [HttpPut("verify/{firmId}")]
    [SwaggerOperation(Summary = "Verify a firm")]
    public async Task<IActionResult> VerifyFirm(Guid firmId)
    {
        return await firmsService.VerifyFirm(firmId);
    }
}