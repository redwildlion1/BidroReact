using Bidro.DTOs.FirmDTOs;

namespace Bidro.Validation.ValidationObjects;

public class FirmValidityObject(PostDTOs.PostFirmDTO firmDTO)
{
    public FirmContactValidityObject FirmContact = new(firmDTO.Contact);
    public FirmLocationValidityObject FirmLocation = new(firmDTO.Location);
    public string Name { get; } = firmDTO.Base.Name;
    public string Description { get; } = firmDTO.Base.Description;
    public string Logo { get; } = firmDTO.Base.Logo;
}

public class FirmLocationValidityObject
{
    public FirmLocationValidityObject(PostDTOs.PostFirmLocationDTO firmLocationDTO)
    {
        Address = firmLocationDTO.Address;
        PostalCode = firmLocationDTO.PostalCode!;
    }

    public FirmLocationValidityObject(UpdateDTOs.UpdateFirmLocationDTO firmLocationDTO)
    {
        Address = firmLocationDTO.Address;
        PostalCode = firmLocationDTO.PostalCode!;
    }

    public string Address { get; }
    public string PostalCode { get; }
}

public class FirmContactValidityObject
{
    public FirmContactValidityObject(PostDTOs.PostFirmContactDTO firmContactDTO)
    {
        Email = firmContactDTO.Email;
        Phone = firmContactDTO.Phone;
    }

    public FirmContactValidityObject(UpdateDTOs.UpdateFirmContactDTO firmContactDTO)
    {
        Email = firmContactDTO.Email;
        Phone = firmContactDTO.Phone;
    }

    public string Email { get; }
    public string Phone { get; }
}

// Database Validity

public class FirmValidityObjectDb(PostDTOs.PostFirmDTO firmDTO)
{
    public FirmBaseValidityObjectDb FirmBase = new(firmDTO.Base);
    public FirmContactValidityObjectDb FirmContact = new(firmDTO.Contact);
    public FirmLocationValidityObjectDb FirmLocation = new(firmDTO.Location);
}

public class FirmBaseValidityObjectDb(PostDTOs.PostFirmBaseDTO firmDTO)
{
    public List<Guid> CategoryIds { get; } = firmDTO.CategoryIds;
    public string Name { get; } = firmDTO.Name;
}

public class FirmLocationValidityObjectDb(PostDTOs.PostFirmLocationDTO firmLocationDTO)
{
    public Guid CityId { get; } = firmLocationDTO.CityId;
    public Guid CountyId { get; } = firmLocationDTO.CountyId;
}

public class FirmContactValidityObjectDb(PostDTOs.PostFirmContactDTO firmContactDTO)
{
    public string Email { get; } = firmContactDTO.Email;
    public string Phone { get; } = firmContactDTO.Phone;
}