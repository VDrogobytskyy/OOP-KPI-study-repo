using System;
public class Client
{
    // Model structure
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool IsValid()
    {
        if (string.IsNullOrEmpty(this.Name)) return false;
        if (!EmailHelper.IsValid(this.Email)) return false;
        if (DateOfBirth > DateTime.Now) return false;
        return true;
    }
}
class ClientRepository
{
    public bool Add(Client client)
    {
    // Persist Data
    using (var cn = new SqlConnection("cnString"))
    {
        var cmd = new SqlCommand("INSERT INTO clients(Name, Email, DateOfBirth) VALUES(@Name, @Email, @DateOfBirth)", cn);
        cmd.Parameters.AddWithValue("Name", client.Name);
        cmd.Parameters.AddWithValue("Email", client.Email);
        cmd.Parameters.AddWithValue("DateOfBirth", client.DateOfBirth);

        cmd.ExecuteNonQuery();
    }
    return true;
    }
}

public class ClientService
{
    public (bool, string) AddClient(Client client)
    {
        if (!client.IsValid()) return (false, "Client data is not valid");

        var repo = new ClientRepository();
        repo.Add(client);
        EmailHelper.Send(client.Email, "Welcome", "Congrats!");
        return (true, string.Empty);
    }
}
public class EmailHelper
{
    public static bool IsValid(string email)
    {
        return email.Contains("@");
    }
    public static void Send(string to, string subject, string body, string from = "no-reply@system.net")
    {
    // Send e-mail
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