using Bidro.Types;

namespace Bidro.DTOs.AuthDTOs;

public class RegisterDTO(string email, string password, string firstName, string lastName, string phoneNumber)
{
    public string Email { get; set; } = email;
    public string Password { get; set; } = password;
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public string PhoneNumber { get; set; } = phoneNumber;
}

public class RegisterUserDatabase(RegisterDTO registerDTO)
{
    public string Email { get; set; } = registerDTO.Email;
    public string FirstName { get; set; } = registerDTO.FirstName;
    public string LastName { get; set; } = registerDTO.LastName;
    public string PhoneNumber { get; set; } = registerDTO.PhoneNumber;

    public string Role { get; set; } = UserRoles.User.ToString();

    public string PasswordHash { get; set; } =
        BCrypt.Net.BCrypt.HashPassword(registerDTO.Password,
            BCrypt.Net.BCrypt.GenerateSalt(12));

    public string SecurityStamp { get; set; } = Guid.NewGuid().ToString();
}

public class LoginDTO(string email, string password)
{
    public string Email { get; set; } = email;
    public string Password { get; set; } = password;
}

public class RefreshTokenDTO(string refreshToken, string email)
{
    public string RefreshToken { get; set; } = refreshToken;
    public string Email { get; set; } = email;
}

public class ForgotPasswordDTO(string email)
{
    public string Email { get; set; } = email;
}

public class ResetPasswordDTO(string email, string token, string newPassword)
{
    public string Email { get; set; } = email;
    public string Token { get; set; } = token;
    public string NewPassword { get; set; } = newPassword;
}

public class ChangePasswordDTO(string oldPassword, string newPassword)
{
    public string OldPassword { get; set; } = oldPassword;
    public string NewPassword { get; set; } = newPassword;
}

public class UpdatePhoneNumberDTO(string phoneNumber)
{
    public string PhoneNumber { get; set; } = phoneNumber;
}

public class VerifyPhoneNumberDTO(string code)
{
    public string Code { get; set; } = code;
}

public class VerifyEmailDTO(string token)
{
    public string Token { get; set; } = token;
}