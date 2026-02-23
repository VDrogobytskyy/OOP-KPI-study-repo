public class ClientService
{
    private readonly IClientRepository clientRepository;
    private readonly IEmailService emailService;

    public ClientService(IClientRepository inp_cl_repo, IEmailService inp_em_serv)
    {
        clientRepository = inp_cl_repo;
        emailService = inp_em_serv;
    }
    public (bool, string) AddClient(Client client)
    {
        if (!client.IsValid()) return (false, "Client data is not valid");

        clientRepository.Add(client);
        emailService.Send(client.Email, "Welcome", "Congrats!");
        return (true, string.Empty);
    }
}