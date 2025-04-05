using Bidro.Config;
using Bidro.DTOs.FirmDTOs;
using Bidro.Services;
using Bidro.Validation.FluentValidators;
using Bidro.Validation.FluentValidators.LittleValidators;
using Bidro.Validation.ValidationObjects;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Bidro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FirmsController(
    IFirmsService firmsService,
    PgConnectionPool pgConnectionPool) : ControllerBase
{
    [HttpGet("{firmId}")]
    [SwaggerOperation(Summary = "Get a firm by its ID")]
    public async Task<IResult> GetFirmById(Guid firmId)
    {
        var validator = VerifyIfExists.FirmExists(pgConnectionPool, firmId);
        if (!await validator) return Results.NotFound("Firm not found");

        var result = await firmsService.GetFirmById(firmId);
        return Results.Ok(result);
    }

    [HttpGet("category/{categoryId}")]
    [SwaggerOperation(Summary = "Get firms in a category")]
    public async Task<IResult> GetFirmsInCategory(Guid categoryId)
    {
        var validator = VerifyIfExists.CategoryExists(pgConnectionPool, categoryId);
        if (!await validator) return Results.NotFound("Category not found");

        var result = await firmsService.GetFirmsInCategory(categoryId);
        return Results.Ok(result);
    }

    [HttpGet("subcategory/{subcategoryId}")]
    [SwaggerOperation(Summary = "Get firms in a subcategory")]
    public async Task<IResult> GetFirmsInSubcategory(Guid subcategoryId)
    {
        var validator = VerifyIfExists.SubcategoryExists(pgConnectionPool, subcategoryId);
        if (!await validator) return Results.NotFound("Subcategory not found");

        var result = await firmsService.GetFirmsInSubcategory(subcategoryId);
        return Results.Ok(result);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Post a new firm")]
    public async Task<IResult> PostFirm(PostFirmDTO postFirmDTO)
    {
        var validator = new FirmValidator(pgConnectionPool);
        var validationResult = await validator.ValidateAsync(new FirmValidityObject(postFirmDTO));
        if (!validationResult.IsValid) return Results.BadRequest(validationResult.Errors);

        var result = await firmsService.PostFirm(postFirmDTO);
        return Results.Created($"/api/firms/{result}", result);
    }

    [HttpPut("updateName")]
    [SwaggerOperation(Summary = "Update firm name")]
    public async Task<IResult> UpdateFirmName(UpdateFirmNameDTO updateFirmNameDTO)
    {
        var validationResult = await new FirmNameValidator(pgConnectionPool).ValidateAsync(updateFirmNameDTO);
        if (!validationResult.IsValid) return Results.BadRequest(validationResult.Errors);
        var result = await firmsService.UpdateFirmName(updateFirmNameDTO);
        return Results.Ok(result);
    }

    [HttpPut("updateDescription")]
    [SwaggerOperation(Summary = "Update firm description")]
    public async Task<IResult> UpdateFirmDescription(UpdateFirmDescriptionDTO updateFirmDescriptionDTO)
    {
        var validationResult = await new FirmDescriptionValidator().ValidateAsync(updateFirmDescriptionDTO);
        if (!validationResult.IsValid) return Results.BadRequest(validationResult.Errors);
        var result = await firmsService.UpdateFirmDescription(updateFirmDescriptionDTO);
        return Results.Ok(result);
    }

    [HttpPut("updateLogo")]
    [SwaggerOperation(Summary = "Update firm logo")]
    public async Task<IResult> UpdateFirmLogo(UpdateFirmLogoDTO updateFirmLogoDTO)
    {
        var result = await firmsService.UpdateFirmLogo(updateFirmLogoDTO);
        return Results.Ok(result);
    }

    [HttpPut("updateWebsite")]
    [SwaggerOperation(Summary = "Update firm website")]
    public async Task<IResult> UpdateFirmWebsite(UpdateFirmWebsiteDTO updateFirmWebsiteDTO)
    {
        var validationResult = await new FirmWebsiteValidator().ValidateAsync(updateFirmWebsiteDTO);
        if (!validationResult.IsValid) return Results.BadRequest(validationResult.Errors);
        var result = await firmsService.UpdateFirmWebsite(updateFirmWebsiteDTO);
        return Results.Ok(result);
    }

    [HttpPut("updateLocation")]
    [SwaggerOperation(Summary = "Update firm location")]
    public async Task<IResult> UpdateFirmLocation(UpdateFirmLocationDTO updateFirmLocationDTO)
    {
        var validationResult = await new FirmLocationValidator(pgConnectionPool)
            .ValidateAsync(new FirmLocationValidityObject(updateFirmLocationDTO));
        if (!validationResult.IsValid) return Results.BadRequest(validationResult.Errors);

        var result = await firmsService.UpdateFirmLocation(updateFirmLocationDTO);
        return Results.Ok(result);
    }

    [HttpPut("updateContact")]
    [SwaggerOperation(Summary = "Update firm contact")]
    public async Task<IResult> UpdateFirmContact(UpdateFirmContactDTO updateFirmContactDTO)
    {
        var validationResult =
            await new FirmContactValidator(pgConnectionPool).ValidateAsync(
                new FirmContactValidityObject(updateFirmContactDTO));
        if (!validationResult.IsValid) return Results.BadRequest(validationResult.Errors);

        var result = await firmsService.UpdateFirmContact(updateFirmContactDTO);
        return Results.Ok(result);
    }

    [HttpPut("suspend/{firmId}")]
    [SwaggerOperation(Summary = "Suspend a firm")]
    public async Task<IResult> SuspendFirm(Guid firmId)
    {
        var result = await firmsService.SuspendFirm(firmId);
        return Results.Ok(result);
    }

    [HttpPut("unsuspend/{firmId}")]
    [SwaggerOperation(Summary = "Unsuspend a firm")]
    public async Task<IResult> UnsuspendFirm(Guid firmId)
    {
        var result = await firmsService.UnsuspendFirm(firmId);
        return Results.Ok(result);
    }

    [HttpPut("verify/{firmId}")]
    [SwaggerOperation(Summary = "Verify a firm")]
    public async Task<IResult> VerifyFirm(Guid firmId)
    {
        var result = await firmsService.VerifyFirm(firmId);
        return Results.Ok(result);
    }
}