public interface INotify
{
    void SendNotify();
}

public class Mail : INotify
{
    public void SendNotify()
    {
        // logic for mail sending
    }
}

public class SMS : INotify
{
    public void SendNotify()
    {
        // logic for SMS sending
    }
}