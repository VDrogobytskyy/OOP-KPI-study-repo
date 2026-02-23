using System;
using System.Collections.Generic;
using System.Text;
public interface IEnroll
{
    void Validate();
    void Persist();
    void SendEmail();
    void SendSMS();
}
class ProductEnroll : IEnroll
{
    public void Persist()
    {
    // Persist to database
    }

    public void SendEmail()
    {
        throw new NotImplementedException("Product don`t have e-mail!");
    }
    public void SendSMS()
    {
        throw new NotImplementedException("Product don`t have phone number!");
    }
    public void Validate()
    {
    // Check data
    }
}
class ContactEnroll : IEnroll
{
    public void Persist()
    {
    // Persist to database
    }
    public void SendEmail()
    {
    // Send email
    }
    public void SendSMS()
    {
    // Send SMS
    }
    public void Validate()
    {
    // Check data
    }
}