using Bidro.DTOs.AuthDTOs;

namespace Bidro.Services;

public interface IAuthService
{
    Task<bool> RegisterAsync(RegisterDTO registerDTO);
    Task<string> LoginAsync(LoginDTO loginDTO);
    Task<string> RefreshTokenAsync(RefreshTokenDTO refreshTokenDTO);
    Task<bool> ForgotPasswordAsync(ForgotPasswordDTO forgotPasswordDTO);
    Task<bool> ResetPasswordAsync(ResetPasswordDTO resetPasswordDTO);
    Task<bool> ChangePasswordAsync(ChangePasswordDTO changePasswordDTO);
    Task<bool> UpdatePhoneNumberAsync(UpdatePhoneNumberDTO updatePhoneNumberDTO);
    Task<bool> VerifyPhoneNumberAsync(VerifyPhoneNumberDTO verifyPhoneNumberDTO);
    Task<bool> VerifyEmailAsync(VerifyEmailDTO verifyEmailDTO);
}