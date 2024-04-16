namespace WarehouseAPI.Shared.Exceptions;
public class NullEntityException : Exception
{
    public NullEntityException(string message)
        : base(message)
    {
    }
}
