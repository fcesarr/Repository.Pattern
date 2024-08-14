namespace Repository.Pattern.Entities;

public abstract class Entity
{
    public Guid Id
    {
        get;
        init;
    } = Guid.NewGuid();

    public DateTime Created
    {
        get;
    } = DateTime.UtcNow;

    public DateTime? Updated
    {
        get;
        set;
    }

    public DateTime? Deleted
    {
        get;
        init;
    }
}
