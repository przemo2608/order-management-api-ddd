namespace OrderManagement.Domain.Exceptions;

public class DomainException(string message) : Exception(message)
{
    public static void ThrowIfNull(object? value, string message)
    {
        if (value is null) throw new DomainException(message);
    }
}
