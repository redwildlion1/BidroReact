using System.Text.RegularExpressions;

namespace Bidro.Notifications;

public partial class EmailNotification : Notification
{
    private string Email { get; }
    
    public EmailNotification(string email, string message, string title, string url) : base(message, title, url)
    { 
        if(CheckEmailNotification(email))
        {
            Email = email;
        }
        else
        {
            throw new ArgumentException("Invalid Email");
        }
    }
    
    private static bool CheckEmailNotification(string email)
    {
        return MailRegex().IsMatch(email) && email.Length < 50;
    }
    
    public void SendEmail()
    {
        // Send email
    }

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    private static partial Regex MailRegex();
}



    