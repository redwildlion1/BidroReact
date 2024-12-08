using Bidro.Users;
using Bidro.Users.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Bidro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController(UserManager<UserTypes.UserAccount> userManager,
    SignInManager<UserTypes.UserAccount> signInManager, IUsersDb usersDb)
    : ControllerBase
{
    [HttpPost("register")]
    [SwaggerOperation(Summary = "Register a new user account")]
    public async Task<IActionResult> Register(UserDTOs.RegisterDTO dto)
    {
        var user = new UserTypes.UserAccount(dto.Username, dto.FirstName, dto.LastName)
        {
            UserName = dto.Username,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber
        };
        var result = await userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded) return Unauthorized();
        await signInManager.SignInAsync(user, isPersistent: false);
        return Ok();

    }
    
    [HttpPost("registerFirmAccount")]
    [SwaggerOperation(Summary = "Register a new firm account")]
    public async Task<IActionResult> RegisterFirmAccount(UserDTOs.RegisterFirmAccountDTO dto)
    {
        var userAccount = new UserTypes.UserAccount(dto.Username, dto.FirstName, dto.LastName)
        {
            UserName = dto.Username,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber
        };
        var firmAccount = new UserTypes.FirmAccount(dto.FirmId);

        var result = await userManager.CreateFirmAccountAsync(userAccount, dto.Password, firmAccount, usersDb);
        if (!result.Succeeded) return Unauthorized();

        await signInManager.SignInAsync(userAccount, isPersistent: false);
        return Ok();
    }

    [HttpPost("registerAdminAccount")]
    [SwaggerOperation(Summary = "Register a new admin account")]
    public async Task<IActionResult> RegisterAdminAccount(UserDTOs.RegisterAdminAccountDTO dto)
    {
        var userAccount = new UserTypes.UserAccount(dto.Username, dto.FirstName, dto.LastName)
        {
            UserName = dto.Username,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber
        };
        var adminAccount = new UserTypes.AdminAccount(dto.CreatedById);

        var result = await userManager.CreateAdminAccountAsync(userAccount, dto.Password, adminAccount, usersDb);
        if (!result.Succeeded) return Unauthorized();

        await signInManager.SignInAsync(userAccount, isPersistent: false);
        return Ok();
    }
    
    [HttpPost("login")]
    [SwaggerOperation(Summary = "Login to an account")]
    public async Task<IActionResult> Login(UserDTOs.LoginDTO dto)
    {
        var result = await signInManager.PasswordSignInAsync(dto.Username, dto.Password, dto.RememberMe, false);
        if (result.Succeeded)
        {
            return Ok();
        }

        return Unauthorized();
    }
    
    [HttpPost("logout")]
    [SwaggerOperation(Summary = "Logout of an account")]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return Ok();
    }
    
}