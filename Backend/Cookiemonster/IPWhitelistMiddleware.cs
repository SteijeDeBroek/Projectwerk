public class IPWhitelistMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<IPWhitelistMiddleware> _logger;
    private readonly List<string> _whitelistedIPs;

    public IPWhitelistMiddleware(RequestDelegate next, ILogger<IPWhitelistMiddleware> logger, IConfiguration configuration)
    {
        _next = next;
        _logger = logger;
        _whitelistedIPs = configuration.GetSection("WhitelistedIPs").Get<List<string>>();
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var requestIP = context.Connection.RemoteIpAddress?.ToString();

        if (_whitelistedIPs.Contains(requestIP))
        {
            await _next(context);
        }
        else
        {
            _logger.LogWarning($"Access denied for IP: {requestIP}");
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return;
        }
    }
}
