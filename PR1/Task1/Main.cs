class Program
{
    static void Main(string[] args)
    {
        var validator = new CL_Validation();

        string where_to_connect = "myServer:\\ip.....";
        var db_conect = new Client_DB_work(where_to_connect);

        var Email_service = new EmailSend();

        var Client_Manager = new Cl_manager(validator, db_conect, Email_service);

        var client = new Client
        {
            Name = "Volodymyr",
            Email = "volodymyr@example.com",
            DateOfBirth = new DateTime(2006, 9, 12)
        };

        var result = Client_Manager.Add(client);

        if (result.result)
        {
            Console.WriteLine("Клієнт успішно доданий!");
        }
        else
        {
            Console.WriteLine("Помилка щось пішло не так!");
        }        

    }

}
