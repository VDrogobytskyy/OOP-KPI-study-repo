
public class CL_Validation
{
    public (bool is_correct_status, string message) Validate(Client client) 
    {
        if (string.IsNullOrEmpty(client.Name)) return (false, "Name is invalid");

        if (!client.Email.Contains("@")) return (false, "Email is invalid");

        if (client.DateOfBirth > DateTime.Now) return (false, "Date of Birth is invalid");

        return(true, "Everything is correct");
    }    
}