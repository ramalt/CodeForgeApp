using System.Runtime.Serialization;

namespace CodeForge.Common.Infrastructure.Exceptions;

public class DbValidationException : Exception
{
    public DbValidationException()
    {
    }

    public DbValidationException(string? message) : base(message)
    {
    }

    public DbValidationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected DbValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
