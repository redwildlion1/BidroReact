using Bidro.EntityObjects;

namespace Bidro.DTOs.FirmDTOs;

public class PostDTOs
{
    public record PostFirmDTO(
        PostFirmBaseDTO Base,
        PostFirmContactDTO Contact,
        PostFirmLocationDTO Location)
    {
        public Firm ToFirm()
        {
            var baseFirm = new Firm(Base.Name, Base.Description, Base.Logo, Base.Website, Base.CategoryIds)
            {
                Contact = Contact.ToFirmContact(),
                Location = Location.ToFirmLocation()
            };

            return baseFirm;
        }
    }

    public record PostFirmBaseDTO(
        string Name,
        string Description,
        string Logo,
        string Website,
        List<Guid> CategoryIds);

    public record PostFirmLocationDTO(
        string Address,
        Guid CityId,
        Guid CountyId,
        string PostalCode,
        string? Latitude,
        string? Longitude)
    {
        public FirmLocation ToFirmLocation()
        {
            return new FirmLocation(Address, CityId, CountyId, PostalCode, Latitude, Longitude);
        }
    }

    public record PostFirmContactDTO(
        string Email,
        string Phone,
        string Fax)
    {
        public FirmContact ToFirmContact()
        {
            return new FirmContact(Email, Phone, Fax);
        }
    }

    public record AddFirmCategoryDTO(
        Guid FirmId,
        string CategoryId);

    public record AddFirmUserDTO(
        Guid FirmId,
        Guid UserId);
}