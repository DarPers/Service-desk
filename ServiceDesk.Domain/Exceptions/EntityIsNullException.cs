namespace ServiceDesk.Domain.Exceptions;
public class EntityIsNullException : Exception
{
    public EntityIsNullException(string message) 
        : base(message)
    {
    }

    public static void ThrowIfNull(object? entity)
    {
        if (entity == null)
        {
            throw new EntityIsNullException("Entity does not exist!");
        }
    }
}
