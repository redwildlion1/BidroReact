using Bidro.Controllers;
using Bidro.DTOs.FirmDTOs;
using Bidro.DTOs.LocationComponentsDTOs;
using Bidro.Services.Implementations;
using Bidro.Validation.FluentValidators;
using Bidro.Validation.ValidationObjects;
using BidroUnitTests.TestsFixtures;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BidroUnitTests.ValidationTests;

// ReSharper disable once ClassNeverInstantiated.Global
public class FirmsValidationTestsFixture : TestFixture<FirmsController, FirmsService>
{
    public FirmsValidationTestsFixture() : base(new FirmsServiceFactory(), new FirmsControllerFactory())
    {
        InitializeAsync().GetAwaiter().GetResult();
    }

    public Guid CountyId { get; private set; }
    public Guid CityId { get; private set; }

    private async Task InitializeAsync()
    {
        var postCountyDTO = new PostCountyDTO("Arges", "AG");
        var locationComponentsService = new LocationComponentsService(DbConnection);
        var addCountyResult = await locationComponentsService.AddCounty(postCountyDTO);
        CountyId = addCountyResult.CountyId;
        var postCityDTO = new PostCityDTO(CountyId, "Pitesti");
        var cityDTO = await locationComponentsService.AddCity(postCityDTO);
        CityId = cityDTO.CityId;
    }
}

public class FirmsValidationTests(FirmsValidationTestsFixture fixture) : IClassFixture<FirmsValidationTestsFixture>
{
    #region FirmBase tests

    [Fact]
    public async Task ValidateFirmBase_ValidAll_ReturnsTrue()
    {
        // Arrange
        var postFirmDTO = new PostFirmBaseDTO("Valid Name", "Description", "Logo", "https://www.google.com/", []);
        var validator = new FirmBaseValidator(fixture.DbConnection);

        // Act
        var result = await validator.ValidateAsync(new FirmBaseValidityObject(postFirmDTO));

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task ValidateFirmBase_EmptyName_ReturnsFalse()
    {
        // Arrange
        var postFirmDTO = new PostFirmBaseDTO("", "Description", "Logo", "https://www.google2.com/", []);
        var validator = new FirmBaseValidator(fixture.DbConnection);

        // Act
        var result = await validator.ValidateAsync(new FirmBaseValidityObject(postFirmDTO));

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(postFirmDTO.Name));
    }

    [Fact]
    public async Task ValidateFirmBase_EmptyDescription_ReturnsFalse()
    {
        // Arrange
        var postFirmDTO = new PostFirmBaseDTO("Valid Name2", "", "Logo", "https://www.google3.com/", []);
        var validator = new FirmBaseValidator(fixture.DbConnection);

        // Act
        var result = await validator.ValidateAsync(new FirmBaseValidityObject(postFirmDTO));

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(postFirmDTO.Description));
    }

    [Fact]
    public async Task ValidateFirmBase_EmptyLogo_ReturnsFalse()
    {
        // Arrange
        var postFirmDTO = new PostFirmBaseDTO("Valid Name3", "Description", "", "https://www.google4.com/", []);
        var validator = new FirmBaseValidator(fixture.DbConnection);

        // Act
        var result = await validator.ValidateAsync(new FirmBaseValidityObject(postFirmDTO));

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(postFirmDTO.Logo));
    }

    [Fact]
    public async Task ValidateFirmBase_EmptyWebsite_ReturnsFalse()
    {
        // Arrange
        var postFirmDTO = new PostFirmBaseDTO("Valid Name4", "Description", "Logo", "", []);
        var validator = new FirmBaseValidator(fixture.DbConnection);

        // Act
        var result = await validator.ValidateAsync(new FirmBaseValidityObject(postFirmDTO));

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(postFirmDTO.Website));
    }

    [Fact]
    //Invalid website
    public async Task ValidateFirmBase_InvalidWebsite_ReturnsFalse()
    {
        // Arrange
        var postFirmDTO = new PostFirmBaseDTO("Valid Name5", "Description", "Logo", "htpps://google5", []);
        var validator = new FirmBaseValidator(fixture.DbConnection);

        // Act
        var result = await validator.ValidateAsync(new FirmBaseValidityObject(postFirmDTO));

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(postFirmDTO.Website));
    }

    [Fact]
    public async Task ValidateFirmBase_DuplicateName_ReturnsFalse()
    {
        var postFirmContactDTO = new PostFirmContactDTO("example72@yahoo.com", "0737210799", "0211111111");
        var postFirmBaseDTO =
            new PostFirmBaseDTO("DUPLICATE", "Description", "Logo", "https://www.DUPLICATENAME.com/", []);
        var postFirmLocationDTO =
            new PostFirmLocationDTO(
                "Crazy address", fixture.CityId, fixture.CountyId,
                "110188", null, null);
        var postFirmDTO = new PostFirmDTO(postFirmBaseDTO, postFirmContactDTO, postFirmLocationDTO);
        var postFirmResult = await fixture.Controller.PostFirm(postFirmDTO);

        Assert.IsType<Created<Guid>>(postFirmResult);
        var duplicateFirmBaseDTO = new PostFirmBaseDTO(
            "DUPLICATE", "Description", "Logo", "https://www.DUPLICATENAME2.com/", []);
        var validator = new FirmBaseValidator(fixture.DbConnection);

        // Act
        var result = await validator.ValidateAsync(new FirmBaseValidityObject(duplicateFirmBaseDTO));

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(duplicateFirmBaseDTO.Name));
    }

    [Fact]
    public async Task ValidateFirmBase_DuplicateWebsite_ReturnsFalse()
    {
        var postFirmContactDTO = new PostFirmContactDTO("example6969@yahoo.com", "0769721079", "0211879116");
        var postFirmBaseDTO =
            new PostFirmBaseDTO("DUPLICATEWEBSITE1", "Description", "Logo", "https://www.DUPLICATEWEBSITE.com/", []);
        var postFirmLocationDTO =
            new PostFirmLocationDTO(
                "Crazy address", fixture.CityId, fixture.CountyId,
                "110188", null, null);
        var postFirmDTO = new PostFirmDTO(postFirmBaseDTO, postFirmContactDTO, postFirmLocationDTO);
        var postFirmResult = await fixture.Controller.PostFirm(postFirmDTO);

        Assert.IsType<Created<Guid>>(postFirmResult);
        var duplicateFirmBaseDTO = new PostFirmBaseDTO(
            "DUPLICATEWEBSITE2", "Description", "Logo", "https://www.DUPLICATEWEBSITE.com/", []);
        var validator = new FirmBaseValidator(fixture.DbConnection);

        // Act
        var result = await validator.ValidateAsync(new FirmBaseValidityObject(duplicateFirmBaseDTO));

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(duplicateFirmBaseDTO.Website));
    }

    #endregion

    #region FirmLocation tests

    [Fact]
    public async Task ValidateFirmLocation_ValidAll_ReturnsTrue()
    {
        // Arrange
        var postFirmLocationDTO =
            new PostFirmLocationDTO(
                "Crazy address", fixture.CityId, fixture.CountyId,
                "110188", null, null);

        var validator = new FirmLocationValidator(fixture.DbConnection);

        // Act
        var result = await validator.ValidateAsync(new FirmLocationValidityObject(postFirmLocationDTO));

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task ValidateFirmLocation_EmptyAddress_ReturnsFalse()
    {
        // Arrange
        var postFirmLocationDTO = new PostFirmLocationDTO(
            "", fixture.CityId, fixture.CountyId,
            "110188", null, null);
        var validator = new FirmLocationValidator(fixture.DbConnection);

        // Act
        var result = await validator.ValidateAsync(new FirmLocationValidityObject(postFirmLocationDTO));

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(postFirmLocationDTO.Address));
    }

    [Fact]
    public async Task ValidateFirmLocation_EmptyPostalCode_ReturnsFalse()
    {
        // Arrange
        var postFirmLocationDTO = new PostFirmLocationDTO(
            "Crazy address", fixture.CityId, fixture.CountyId,
            "", null, null);
        var validator = new FirmLocationValidator(fixture.DbConnection);

        // Act
        var result = await validator.ValidateAsync(new FirmLocationValidityObject(postFirmLocationDTO));

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(postFirmLocationDTO.PostalCode));
    }

    [Fact]
    public async Task ValidateFirmLocation_EmptyCityId_ReturnsFalse()
    {
        // Arrange
        var postFirmLocationDTO = new PostFirmLocationDTO(
            "Crazy address", Guid.Empty, fixture.CountyId,
            "110188", null, null);
        var validator = new FirmLocationValidator(fixture.DbConnection);

        // Act
        var result = await validator.ValidateAsync(new FirmLocationValidityObject(postFirmLocationDTO));

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(postFirmLocationDTO.CityId));
    }

    [Fact]
    public async Task ValidateFirmLocation_EmptyCountyId_ReturnsFalse()
    {
        // Arrange
        var postFirmLocationDTO = new PostFirmLocationDTO(
            "Crazy address", fixture.CityId, Guid.Empty,
            "110188", null, null);
        var validator = new FirmLocationValidator(fixture.DbConnection);

        // Act
        var result = await validator.ValidateAsync(new FirmLocationValidityObject(postFirmLocationDTO));

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(postFirmLocationDTO.CountyId));
    }

    #endregion

    #region FirmContact tests

    [Fact]
    public async Task ValidateFirmContact_ValidAll_ReturnsTrue()
    {
        // Arrange
        var postFirmContactDTO = new PostFirmContactDTO("examplevalid@yahoo.com", "0713676911", "0713076911");
        var validator = new FirmContactValidator(fixture.DbConnection);

        // Act
        var result = await validator.ValidateAsync(new FirmContactValidityObject(postFirmContactDTO));

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task ValidateFirmContact_EmptyEmail_ReturnsFalse()
    {
        // Arrange
        var postFirmContactDTO = new PostFirmContactDTO("", "0711111112", "0269111111");
        var validator = new FirmContactValidator(fixture.DbConnection);

        // Act
        var result = await validator.ValidateAsync(new FirmContactValidityObject(postFirmContactDTO));

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(postFirmContactDTO.Email));
    }

    [Fact]
    public async Task ValidateFirmContact_EmptyPhone_ReturnsFalse()
    {
        // Arrange
        var postFirmContactDTO = new PostFirmContactDTO("example3@yahoo.com", "", "0211111113");
        var validator = new FirmContactValidator(fixture.DbConnection);

        // Act
        var result = await validator.ValidateAsync(new FirmContactValidityObject(postFirmContactDTO));

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(postFirmContactDTO.Phone));
    }

    [Fact]
    public async Task ValidateFirmContact_InvalidEmail_ReturnsFalse()
    {
        // Arrange
        var postFirmContactDTO = new PostFirmContactDTO("example4", "0711111114", "0211111114");
        var validator = new FirmContactValidator(fixture.DbConnection);

        // Act
        var result = await validator.ValidateAsync(new FirmContactValidityObject(postFirmContactDTO));

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(postFirmContactDTO.Email));
    }

    [Fact]
    public async Task ValidateFirmContact_InvalidPhone_ReturnsFalse()
    {
        // Arrange
        var postFirmContactDTO = new PostFirmContactDTO("example5@yahoo.com", "07111111", "0211111115");
        var validator = new FirmContactValidator(fixture.DbConnection);

        // Act
        var result = await validator.ValidateAsync(new FirmContactValidityObject(postFirmContactDTO));

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(postFirmContactDTO.Phone));
    }

    [Fact]
    public async Task ValidateFirmContact_DuplicatePhone_ReturnsFalse()
    {
        // Arrange
        var postFirmContactDTO = new PostFirmContactDTO("example6@yahoo.com", "0733000757", "0211691115");
        var postFirmBaseDTO =
            new PostFirmBaseDTO("DUPLICATEPHONE", "Description", "Logo", "https://www.DUPLICATEPHONE.com/", []);
        var postFirmLocationDTO =
            new PostFirmLocationDTO(
                "Crazy address", fixture.CityId, fixture.CountyId,
                "110188", null, null);
        var postFirmDTO = new PostFirmDTO(postFirmBaseDTO, postFirmContactDTO, postFirmLocationDTO);
        var postFirmResult = await fixture.Controller.PostFirm(postFirmDTO);

        Assert.IsType<Created<Guid>>(postFirmResult);

        var duplicateFirmContactDTO = new PostFirmContactDTO("example7@yahoo.com", "0733000757", "0211701115");
        var validator = new FirmContactValidator(fixture.DbConnection);

        // Act
        var result = await validator.ValidateAsync(new FirmContactValidityObject(duplicateFirmContactDTO));

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(duplicateFirmContactDTO.Phone));
    }

    [Fact]
    public async Task ValidateFirmContact_DuplicateEmail_ReturnsFalse()
    {
        var postFirmContactDTO = new PostFirmContactDTO("example69@yahoo.com", "0733010799", "0211111116");
        var postFirmBaseDTO =
            new PostFirmBaseDTO("Valid Name79", "Description", "Logo", "https://www.google69.com/", []);
        var postFirmLocationDTO =
            new PostFirmLocationDTO(
                "Crazy address", fixture.CityId, fixture.CountyId,
                "110188", null, null);
        var postFirmDTO = new PostFirmDTO(postFirmBaseDTO, postFirmContactDTO, postFirmLocationDTO);
        var postFirmResult = await fixture.Controller.PostFirm(postFirmDTO);

        Assert.IsType<Created<Guid>>(postFirmResult);
        var duplicateFirmContactDTO = new PostFirmContactDTO("example69@yahoo.com", "0792010757", "0211113117");
        var validator = new FirmContactValidator(fixture.DbConnection);

        // Act
        var result = await validator.ValidateAsync(new FirmContactValidityObject(duplicateFirmContactDTO));

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(duplicateFirmContactDTO.Email));
    }

    #endregion
}