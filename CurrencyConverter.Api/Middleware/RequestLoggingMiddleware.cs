using System.Diagnostics;
using System.Security.Claims;
namespace CurrencyConverter.Api.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(
            RequestDelegate next,
            ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            // Correlation Id
            var correlationId = Guid.NewGuid().ToString();
            context.Response.Headers["X-Correlation-Id"] = correlationId;

            await _next(context);

            stopwatch.Stop();

            var clientIp = context.Connection.RemoteIpAddress?.ToString();
            var method = context.Request.Method;
            var path = context.Request.Path;
            var statusCode = context.Response.StatusCode;
            var responseTime = stopwatch.ElapsedMilliseconds;

            var clientId = context.User?.Claims
                .FirstOrDefault(c => c.Type == "clientId")?.Value;

            _logger.LogInformation(
                "Request {Method} {Path} | Status: {StatusCode} | Time: {Elapsed}ms | IP: {IP} | ClientId: {ClientId} | CorrelationId: {CorrelationId}",
                method,
                path,
                statusCode,
                responseTime,
                clientIp,
                clientId,
                correlationId
            );
        }
    }
}
