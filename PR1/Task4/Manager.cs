public class EnrollManager
{
    private readonly INotify _notify;
    private readonly IEnroll _enroll;

    public EnrollManager(INotify notify = null, IEnroll enroll)
    {
        _notify = notify;
        _enroll = enroll;
    }

    public void Sending()
    {
        if(_enroll.Validate() && _enroll.Persist()){
            _notify?.SendNotify();
        }
    }


}