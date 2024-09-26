using Application.Common.Exceptions;

namespace API.Services;

public class ApiGuard(IHttpContextAccessor httpContextAccessor)
{
    public void ThrowIfIdMismatch(int expectedId, int actualId)
    {
        if (expectedId != actualId)
        {
            var path = httpContextAccessor.HttpContext?.Request.Path.ToString();
            throw new ApiGuardException($"The provided key '{actualId}' does not match the expected key '{expectedId}'.")
            {
                Data = { ["Instance"] = path }
            };
        }
    }
}
