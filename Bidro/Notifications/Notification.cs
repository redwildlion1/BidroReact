namespace Bidro.Notifications;

public class Notification
{
    private string Message { get; }
    private string Title { get; }
    private string Url { get; }

    public Notification(string message, string title, string url)
    {
        if(CheckNotification(message, title, url))
        {
            Message = message;
            Title = title;
            Url = url;
        }
        else
        {
            throw new ArgumentException("Invalid Notification");
        }
    }
    
    private static bool CheckNotification(string message, string title, string url)
    {
        return CheckMessageNotification(message)
               && CheckTitleNotification(title)
               && CheckUrlNotification(url);
    }
    
    private static bool CheckUrlNotification(string url)
    {
        return url.Length > 0;
    }
    
    private static bool CheckTitleNotification(string title)
    {
        return title.Length > 0;
    }
    
    private static bool CheckMessageNotification(string message)
    {
        return message.Length > 0;
    }
    
    
}