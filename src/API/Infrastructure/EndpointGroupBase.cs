using API.Services;

namespace API.Infrastructure;

public abstract class EndpointGroupBase
{
    public abstract void Map(WebApplication app);

    private ApiGuard? _apiGuard;

    /// <summary>
    /// Lazy-loaded ApiGuard service
    /// </summary>
    protected ApiGuard ApiGuard => _apiGuard
        ??= HttpContext?.RequestServices.GetService<ApiGuard>()
        ?? throw new InvalidOperationException("ApiGuard is not available.");

    // You may need to access HttpContext in minimal API, so inject IHttpContextAccessor
    private IHttpContextAccessor? _httpContextAccessor;

    protected HttpContext? HttpContext => _httpContextAccessor?.HttpContext;

    public void SetHttpContextAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
}
