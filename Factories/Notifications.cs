namespace DesignPatternsPlayground;

public interface INotifier
{
    void Notify(string userId, string message);
}

public sealed class EmailNotifier : INotifier
{
    public void Notify(string userId, string message)
    {
        Console.WriteLine($"EmailNotifier: sent to {userId}: {message}");
    }
}

public sealed class SmsNotifier : INotifier
{
    public void Notify(string userId, string message)
    {
        Console.WriteLine($"SmsNotifier: sent to {userId}: {message}");
    }
}

public sealed class NotificationFactory
{
    public INotifier Create(string channel)
    {
        return channel.ToLowerInvariant() switch
        {
            "email" => new EmailNotifier(),
            "sms" => new SmsNotifier(),
            _ => throw new NotSupportedException($"Notification channel '{channel}' is not supported.")
        };
    }
}
