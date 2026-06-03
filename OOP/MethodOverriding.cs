//Method Overriding (or Runtime (Dynamic) Polymorphism):
using System;
using System.Collections.Generic;

public class Notification
{
    protected string Recipient;
    protected string Message;

    public Notification(string recipient, string message)
    {
        Recipient = recipient;
        Message = message;
    }

    public virtual void Send()
    {
        Console.WriteLine($"Sending generic notification to {Recipient}");
    }
}

public class EmailNotification : Notification
{
    private string _subject;

    public EmailNotification(string recipient, string message, string subject)
        : base(recipient, message)
    {
        _subject = subject;
    }

    public override void Send()
    {
        Console.WriteLine($"Sending EMAIL to {Recipient} | Subject: {_subject}");
    }
}

public class SMSNotification : Notification
{
    private string _phoneNumber;

    public SMSNotification(string recipient, string message, string phoneNumber)
        : base(recipient, message)
    {
        _phoneNumber = phoneNumber;
    }

    public override void Send()
    {
        Console.WriteLine($"Sending SMS to {_phoneNumber} | Message: {Message}");
    }
}

public class PushNotification : Notification
{
    private string _deviceToken;

    public PushNotification(string recipient, string message, string deviceToken)
        : base(recipient, message)
    {
        _deviceToken = deviceToken;
    }

    public override void Send()
    {
        Console.WriteLine($"Sending PUSH to device {_deviceToken.Substring(0, 8)}"
            + $"... | Alert: {Message}");
    }
}

public class Program
{
    public static void Main()
    {
        var notifications = new List<Notification>
        {
            new EmailNotification("alice@example.com", "Your order shipped!", "Order Update"),
            new SMSNotification("Bob", "Code: 482910", "+1-555-0123"),
            new PushNotification("Charlie", "New message", "d8a3f4b2c1e5a9b7")
        };

        foreach (var n in notifications)
        {
            n.Send();
        }
    }
}
