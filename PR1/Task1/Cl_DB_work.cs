
public class Client_DB_work
{
    private readonly string connect_to;

    public Client_DB_work(string con_str)
    {
        connect_to = con_str;
    }

    public void Add(Client client)
    {
        using var cn = new SqlConnection(connect_to);
        cn.open();


        var cmd = new SqlCommand("INSERT INTO clients(Name, Email, DateOfBirth) VALUES(@Name, @Email, @DateOfBirth)", cn);
        
        cmd.Parameters.AddWithValue("Name", client.Name);
        cmd.Parameters.AddWithValue("Email", client.Email);
        cmd.Parameters.AddWithValue("DateOfBirth", client.DateOfBirth);

        cmd.ExecuteNonQuery();
        
    }
}