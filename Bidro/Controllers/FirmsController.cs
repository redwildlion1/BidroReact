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
        var result = await firmsService.GetFirmById(firmId);
        return Ok(result);
    }

    [HttpGet("category/{categoryId}")]
    [SwaggerOperation(Summary = "Get firms in a category")]
    public async Task<IActionResult> GetFirmsInCategory(Guid categoryId)
    {
        var result = await firmsService.GetFirmsInCategory(categoryId);
        return Ok(result);
    }

    [HttpGet("subcategory/{subcategoryId}")]
    [SwaggerOperation(Summary = "Get firms in a subcategory")]
    public async Task<IActionResult> GetFirmsInSubcategory(Guid subcategoryId)
    {
        var result = await firmsService.GetFirmsInSubcategory(subcategoryId);
        return Ok(result);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Post a new firm")]
    public async Task<IActionResult> PostFirm(PostDTOs.PostFirmDTO postFirmDTO)
    {
        var result = await firmsService.PostFirm(postFirmDTO);
        return CreatedAtAction(nameof(GetFirmById), new { firmId = result }, result);
    }

    [HttpPut("updateName")]
    [SwaggerOperation(Summary = "Update firm name")]
    public async Task<IActionResult> UpdateFirmName(UpdateDTOs.UpdateFirmNameDTO updateFirmNameDTO)
    {
        var result = await firmsService.UpdateFirmName(updateFirmNameDTO);
        return Ok(result);
    }

    [HttpPut("updateDescription")]
    [SwaggerOperation(Summary = "Update firm description")]
    public async Task<IActionResult> UpdateFirmDescription(UpdateDTOs.UpdateFirmDescriptionDTO updateFirmDescriptionDTO)
    {
        var result = await firmsService.UpdateFirmDescription(updateFirmDescriptionDTO);
        return Ok(result);
    }

    [HttpPut("updateLogo")]
    [SwaggerOperation(Summary = "Update firm logo")]
    public async Task<IActionResult> UpdateFirmLogo(UpdateDTOs.UpdateFirmLogoDTO updateFirmLogoDTO)
    {
        var result = await firmsService.UpdateFirmLogo(updateFirmLogoDTO);
        return Ok(result);
    }

    [HttpPut("updateWebsite")]
    [SwaggerOperation(Summary = "Update firm website")]
    public async Task<IActionResult> UpdateFirmWebsite(UpdateDTOs.UpdateFirmWebsiteDTO updateFirmWebsiteDTO)
    {
        var result = await firmsService.UpdateFirmWebsite(updateFirmWebsiteDTO);
        return Ok(result);
    }

    [HttpPut("updateLocation")]
    [SwaggerOperation(Summary = "Update firm location")]
    public async Task<IActionResult> UpdateFirmLocation(UpdateDTOs.UpdateFirmLocationDTO updateFirmLocationDTO)
    {
        var result = await firmsService.UpdateFirmLocation(updateFirmLocationDTO);
        return Ok(result);
    }

    [HttpPut("updateContact")]
    [SwaggerOperation(Summary = "Update firm contact")]
    public async Task<IActionResult> UpdateFirmContact(UpdateDTOs.UpdateFirmContactDTO updateFirmContactDTO)
    {
        var result = await firmsService.UpdateFirmContact(updateFirmContactDTO);
        return Ok(result);
    }

    [HttpPut("suspend/{firmId}")]
    [SwaggerOperation(Summary = "Suspend a firm")]
    public async Task<IActionResult> SuspendFirm(Guid firmId)
    {
        var result = await firmsService.SuspendFirm(firmId);
        return Ok(result);
    }

    [HttpPut("unsuspend/{firmId}")]
    [SwaggerOperation(Summary = "Unsuspend a firm")]
    public async Task<IActionResult> UnsuspendFirm(Guid firmId)
    {
        var result = await firmsService.UnsuspendFirm(firmId);
        return Ok(result);
    }

    [HttpPut("verify/{firmId}")]
    [SwaggerOperation(Summary = "Verify a firm")]
    public async Task<IActionResult> VerifyFirm(Guid firmId)
    {
        var result = await firmsService.VerifyFirm(firmId);
        return Ok(result);
    }
}