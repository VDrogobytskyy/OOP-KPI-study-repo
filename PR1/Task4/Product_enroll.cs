public interface IEnroll
{
    bool Validate();
    bool Persist();
}


public class ProductEnroll : IEnroll
{
    public bool Validate()
    {
    // Check data
    }

    public bool Persist()
    {
    // Persist to database
    }
}

public class ContactEnroll : IEnroll
{
    public bool Validate()
    {
    // Check data
    }

    public bool Persist()
    {
    // Persist to database
    }
}

