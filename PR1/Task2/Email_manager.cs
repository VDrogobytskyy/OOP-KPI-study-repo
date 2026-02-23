public interface IEmailService
{
    public static void Send(string to, string subject, string body, string from = "no-reply@system.net");
}

public class EmailHelper : IEmailService
{
    public static bool IsValid(string email)
    {
        return email.Contains("@");
    }
    public static void Send(string to, string subject, string body, string from = "no-reply@system.net")
    {
    var mail = new MailMessage(from, to);
    var smtpClient = new SmtpClient
    {
        Port = 25,
        Host = "smtp.system.net"
    };

        mail.Subject = subject;
        mail.Body = body;
        smtpClient.Send(mail);
    }
}