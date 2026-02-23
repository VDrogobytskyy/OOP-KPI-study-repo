

public class EmailSend
{
    public SendEmail_to_Cl(string email_where_to_send)
    {
        var mail = new MailMessage("no-reply@system.net", email_where_to_send);

        var smtpClient = new SmtpClient
        {
            Port = 25,
            Host = "smtp.system.net"
        };

        mail.Subject = "[System.NET] Welcome";
        mail.Body = "Congrats!";

        smtpClient.Send(mail);
    }
}