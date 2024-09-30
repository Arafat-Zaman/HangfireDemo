public class EmailService
{
    public void SendEmail(string to, string subject, string body)
    {
        // In a real application, you'd integrate with an email provider here.
        // This is just for demonstration purposes.
        Console.WriteLine($"Email sent to: {to}, Subject: {subject}, Body: {body}");
    }
}
