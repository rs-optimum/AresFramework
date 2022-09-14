namespace AresFramework.Cache.Exceptions;

public class InvalidCacheException : Exception
{
    public InvalidCacheException(string? message) : base(message)
    {
    }
}