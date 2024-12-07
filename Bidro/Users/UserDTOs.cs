namespace Bidro.Users;

public static class UserDTOs
{
    public class RegisterDTO(string email, string password, string username,
        string phoneNumber, string firstName, string lastName)
    {
        public string Username { get; set; } = username;
        public string Email { get; set; } = email;
        public string Password { get; set; } = password;
        public string PhoneNumber { get; set; } = phoneNumber;
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
    }
    
    public class LoginDTO(string username,string password, bool rememberMe)
    {
        public string Username { get; set; } = username;
        public string Password { get; set; } = password;
        public bool RememberMe { get; set; } = rememberMe;
    }
    
    public class RegisterFirmAccountDTO(string email, string password, string username,
        string phoneNumber, string firstName, string lastName, Guid firmId)
    {
        public string Username { get; set; } = username;
        public string Email { get; set; } = email;
        public string Password { get; set; } = password;
        public string PhoneNumber { get; set; } = phoneNumber;
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
        public Guid FirmId { get; set; } = firmId;
    }

    public class RegisterAdminAccountDTO(string email, string password, string username,
        string phoneNumber, string firstName, string lastName, Guid createdById)
    {
        public string Username { get; set; } = username;
        public string Email { get; set; } = email;
        public string Password { get; set; } = password;
        public string PhoneNumber { get; set; } = phoneNumber;
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
        public Guid CreatedById { get; set; } = createdById;
    }
}