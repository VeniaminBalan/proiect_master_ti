namespace MVC.Entities;

public abstract class BaseEntity<T>
{
    public T Id { get; private set; } = default!;
    
    protected BaseEntity() { }
    
    protected BaseEntity(T id)
    {
        Id = id;
    }
}