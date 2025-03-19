namespace Bidro.DTOs.FirmDTOs;

public class UpdateDTOs
{
    public record UpdateFirmLocationDTO(
        Guid Id,
        Guid CityId,
        Guid CountyId,
        string Address,
        string PostalCode,
        string? Latitude,
        string? Longitude);

    public record UpdateFirmContactDTO(
        Guid Id,
        string Email,
        string Phone,
        string Fax);

    public record UpdateFirmNameDTO(
        Guid FirmId,
        string Name);

    public record UpdateFirmDescriptionDTO(
        Guid FirmId,
        string Description);

    public record UpdateFirmLogoDTO(
        Guid FirmId,
        string Logo);

    public record UpdateFirmWebsiteDTO(
        Guid FirmId,
        string Website);
}