namespace NorthwindTradeSuite.API.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            string logInformationMessage = $"Path: {httpContext.Request.Path}, Method: {httpContext.Request.Method}, Body: {httpContext.Request.BodyReader}";

            _logger.LogInformation(logInformationMessage);

            await _next(httpContext);
        }
    }
}
