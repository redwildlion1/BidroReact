namespace Bidro.Firms.DTOs;

public record AddFirmDTO(
    string Name,
    string Description,
    string Logo,
    string? Website,
    List<string>? CategoryIds,
    FirmContactDTO Contact,
    FirmLocationDTO Location)
{
};