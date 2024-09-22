using System.Diagnostics.CodeAnalysis;

namespace Application.Common.Exceptions;

public static class Guard
{
    public static void NotFound<T>(object? key, [NotNull]  T entity)
    {
        if (entity is null)
        {
            throw new NotFoundException(key?.ToString() ?? "UnknownKey", typeof(T).Name);
        }
    }
}
