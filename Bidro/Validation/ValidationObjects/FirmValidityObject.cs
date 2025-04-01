using Bidro.DTOs.FirmDTOs;

namespace Bidro.Validation.ValidationObjects;

public class FirmValidityObject(PostDTOs.PostFirmDTO firmDTO)
{
    public FirmBaseValidityObject FirmBase = new(firmDTO.Base);
    public FirmContactValidityObject FirmContact = new(firmDTO.Contact);
    public FirmLocationValidityObject FirmLocation = new(firmDTO.Location);
}

public class FirmBaseValidityObject(PostDTOs.PostFirmBaseDTO firmDTO)
{
    public List<Guid> CategoryIds { get; } = firmDTO.SubcategoryIds;
    public string Name { get; } = firmDTO.Name;
    public string Description { get; } = firmDTO.Description;
    public string Logo { get; } = firmDTO.Logo;
}

public class FirmLocationValidityObject(PostDTOs.PostFirmLocationDTO firmLocationDTO)
{
    public Guid CityId { get; } = firmLocationDTO.CityId;
    public Guid CountyId { get; } = firmLocationDTO.CountyId;

    public string Address { get; } = firmLocationDTO.Address;
    public string PostalCode { get; } = firmLocationDTO.PostalCode!;
}

public class FirmContactValidityObject(PostDTOs.PostFirmContactDTO firmContactDTO)
{
    public string Email { get; } = firmContactDTO.Email;
    public string Phone { get; } = firmContactDTO.Phone;
}