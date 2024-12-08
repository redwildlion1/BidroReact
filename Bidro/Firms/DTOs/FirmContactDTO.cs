namespace Bidro.Firms.DTOs;

public record FirmContactDTO(string Email, string Phone, string Fax)
{
    public FirmContact ToFirmContact()
    {
        return new FirmContact(Email, Phone , Fax);
    }
}