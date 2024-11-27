using System.Text.RegularExpressions;

namespace Bidro.Notifications;

public partial class SmsNotification : Notification
{
    private string PhoneNumber { get; }
    
    public SmsNotification(string phoneNumber, string message, string title, string url) : base(message, title, url)
    { 
        if(CheckSmsNotification(phoneNumber))
        {
            PhoneNumber = phoneNumber;
        }
        else
        {
            throw new ArgumentException("Invalid Phone Number");
        }
    }
    
    private static bool CheckSmsNotification(string phoneNumber)
    {
        return PhoneNumberRegex().IsMatch(phoneNumber) && phoneNumber.Length < 50;
    }
    
    public void SendSms()
    {
        // Send sms
    }

    [GeneratedRegex(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$")]
    private static partial Regex PhoneNumberRegex();
}