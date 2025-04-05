using Bidro.DTOs.FirmDTOs;

namespace Bidro.Validation.ValidationObjects;

public class FirmValidityObject(PostFirmDTO firmDTO)
{
    public readonly FirmBaseValidityObject FirmBase = new(firmDTO.Base);
    public readonly FirmContactValidityObject FirmContact = new(firmDTO.Contact);
    public readonly FirmLocationValidityObject FirmLocation = new(firmDTO.Location);
}

public class FirmBaseValidityObject(PostFirmBaseDTO firmDTO)
{
    public List<Guid> SubcategoryIds { get; } = firmDTO.SubcategoryIds;
    public string Name { get; } = firmDTO.Name;
    public string Description { get; } = firmDTO.Description;
    public string Logo { get; } = firmDTO.Logo;

    public string Website { get; } = firmDTO.Website;
}

public class FirmLocationValidityObject
{
    public FirmLocationValidityObject(PostFirmLocationDTO firmLocationDTO)
    {
        CityId = firmLocationDTO.CityId;
        CountyId = firmLocationDTO.CountyId;
        Address = firmLocationDTO.Address;
        PostalCode = firmLocationDTO.PostalCode!;
    }

    public FirmLocationValidityObject(UpdateFirmLocationDTO firmLocationDTO)
    {
        CityId = firmLocationDTO.CityId;
        CountyId = firmLocationDTO.CountyId;
        Address = firmLocationDTO.Address;
        PostalCode = firmLocationDTO.PostalCode!;
    }

    public Guid CityId { get; }
    public Guid CountyId { get; }

    public string Address { get; }
    public string PostalCode { get; }
}

public class FirmContactValidityObject
{
    public FirmContactValidityObject(PostFirmContactDTO firmContactDTO)
    {
        Email = firmContactDTO.Email;
        Phone = firmContactDTO.Phone;
        Fax = firmContactDTO.Fax;
    }

    public FirmContactValidityObject(UpdateFirmContactDTO firmContactDTO)
    {
        Email = firmContactDTO.Email;
        Phone = firmContactDTO.Phone;
        Fax = firmContactDTO.Fax;
    }

    public string Email { get; }
    public string Phone { get; }
    public string Fax { get; }
}