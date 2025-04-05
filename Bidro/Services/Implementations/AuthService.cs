using Bidro.Config;
using Bidro.DTOs.AuthDTOs;
using Dapper;

namespace Bidro.Services.Implementations;

public class AuthService(PgConnectionPool pgConnectionPool) : IAuthService
{
    public async Task<bool> RegisterAsync(RegisterDTO registerDTO)
    {
        var registerUserDatabase = new RegisterUserDatabase(registerDTO);

        using var db = await pgConnectionPool.RentAsync();
        const string sql =
            "INSERT INTO \"Users\" (\"Email\", \"PasswordHash\", \"FirstName\", \"LastName\", \"PhoneNumber\", \"Role\", \"SecurityStamp\") " +
            "VALUES (@Email, @PasswordHash, @FirstName, @LastName, @PhoneNumber, @Role, @SecurityStamp)";
        var result = await db.ExecuteAsync(sql, registerUserDatabase);
        return result > 0;
    }

    public async Task<string> LoginAsync(LoginDTO loginDTO)
    {
        using var db = await pgConnectionPool.RentAsync();
        const string sql = "SELECT \"PasswordHash\" FROM \"Users\" WHERE \"Email\" = @Email";
        var passwordHash = await db.QuerySingleOrDefaultAsync<string>(sql, new { loginDTO.Email });

        if (passwordHash == null || !BCrypt.Net.BCrypt.Verify(loginDTO.Password, passwordHash))
            throw new UnauthorizedAccessException("Invalid email or password.");

        //TODO: Generate JWT token here
        return "GeneratedJWTToken"; // Placeholder for the actual token generation logic
    }

    public async Task<string> RefreshTokenAsync(RefreshTokenDTO refreshTokenDTO)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ForgotPasswordAsync(ForgotPasswordDTO forgotPasswordDTO)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ResetPasswordAsync(ResetPasswordDTO resetPasswordDTO)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ChangePasswordAsync(ChangePasswordDTO changePasswordDTO)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdatePhoneNumberAsync(UpdatePhoneNumberDTO updatePhoneNumberDTO)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> VerifyPhoneNumberAsync(VerifyPhoneNumberDTO verifyPhoneNumberDTO)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> VerifyEmailAsync(VerifyEmailDTO verifyEmailDTO)
    {
        throw new NotImplementedException();
    }
}