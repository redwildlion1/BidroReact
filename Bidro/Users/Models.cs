namespace Bidro.Users;

public static class Models
{
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