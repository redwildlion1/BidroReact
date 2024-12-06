using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bidro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        var user = new IdentityUser
        {
            UserName = model.Email,
            Email = model.Email
        };
        var result = await userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded) return Unauthorized();
        await signInManager.SignInAsync(user, isPersistent: false);
        return Ok();

    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginModel model)
    {
        var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
        if (result.Succeeded)
        {
            return Ok();
        }

        return Unauthorized();
    }
    
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return Ok();
    }
    
    public class RegisterModel(string email, string password)
    {
        public string Email { get; set; } = email;
        public string Password { get; set; } = password;
    }
    
    public class LoginModel(string email, string password, bool rememberMe)
    {
        public string Email { get; set; } = email;
        public string Password { get; set; } = password;
        public bool RememberMe { get; set; } = rememberMe;
    }
}