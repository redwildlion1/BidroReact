using Bidro.EntityObjects;

namespace Bidro.DTOs.FirmDTOs;

public class GetDTOs
{
    public record GetFirmLocationDTO(
        string Address,
        Guid CityId,
        Guid CountyId,
        string? PostalCode,
        string? Latitude,
        string? Longitude)
    {
        public static GetFirmLocationDTO FromFirmLocation(FirmLocation firmLocation)
        {
            return new GetFirmLocationDTO(firmLocation.Address, firmLocation.CityId, firmLocation.CountyId,
                firmLocation.PostalCode, firmLocation.Latitude, firmLocation.Longitude);
        }
    }

    public record GetFirmContactDTO(string Email, string Phone, string Fax)
    {
        public static GetFirmContactDTO FromFirmContact(FirmContact firmContact)
        {
            return new GetFirmContactDTO(firmContact.Email, firmContact.Phone, firmContact.Fax);
        }
    }

    public record GetFirmDTO(
        Guid Id,
        string Name,
        string Description,
        string Logo,
        string? Website,
        List<Guid>? CategoryIds,
        GetFirmContactDTO Contact,
        GetFirmLocationDTO Location)
    {
        public GetFirmDTO FromFirm(Firm firm)
        {
            return new GetFirmDTO(firm.Id, firm.Name, firm.Description, firm.Logo!, firm.Website,
                firm.CategoryIds, GetFirmContactDTO.FromFirmContact(firm.Contact!),
                GetFirmLocationDTO.FromFirmLocation(firm.Location!));
        }
    }

    public record GetFirmForListDisplayDTO(
        Guid Id,
        string Name,
        string Description,
        string Logo,
        int Rating,
        GetFirmLocationDTO Location,
        GetFirmContactDTO Contact)
    {
        public GetFirmForListDisplayDTO FromFirm(Firm firm)
        {
            return new GetFirmForListDisplayDTO(firm.Id, firm.Name, firm.Description, firm.Logo!, firm.Rating,
                GetFirmLocationDTO.FromFirmLocation(firm.Location!), GetFirmContactDTO.FromFirmContact(firm.Contact!));
        }

        public static List<GetFirmForListDisplayDTO> FromFirms(List<Firm> firms)
        {
            return firms.Select(firm => new GetFirmForListDisplayDTO(firm.Id, firm.Name, firm.Description, firm.Logo!,
                    firm.Rating,
                    GetFirmLocationDTO.FromFirmLocation(firm.Location!),
                    GetFirmContactDTO.FromFirmContact(firm.Contact!)))
                .ToList();
        }
    }
}