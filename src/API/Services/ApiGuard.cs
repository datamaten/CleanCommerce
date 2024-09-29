using Application.Common.Exceptions;

namespace API.Services;

public class ApiGuard(IHttpContextAccessor httpContextAccessor) : IApiGuard
{
    public void ValidateIds(int expectedId, int actualId)
    {
        if (expectedId != actualId)
        {
            var path = httpContextAccessor.HttpContext?.Request.Path.ToString();
            throw new ApiGuardException($"The route id '{expectedId}' in '{path}' does not match the id '{actualId}' provided in the request.");
        }
    }
}
