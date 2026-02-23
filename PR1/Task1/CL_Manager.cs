
public class Cl_manager{
    private readonly CL_Validation _validation_serv;
    private readonly Client_DB_work _db_serv;
    private readonly EmailSend _email_serv;

    public Cl_manager(CL_Validation val, Client_DB_work db, EmailSend em)
    {
        _validation_serv = val;
        _db_serv = db;
        _email_serv = em;
    }

    public (bool result, string message) Add_Client(Client client)
    {
        var is_valid = _validation_serv.Validate(client);
        if (!is_valid.is_correct_status)
        {
            return (false, "Error validation");
        }
        else
        {
            _db_serv.Add(client);
            _email_serv.SendEmail_to_Cl(client.Email);
        }

        return (true, string.Empty);

    }

}