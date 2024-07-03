using Microsoft.AspNetCore.Mvc;
using NorthwindTradeSuite.API.Middlewares.Contracts;
using System.Net;
using System.Text.Json;

namespace NorthwindTradeSuite.API.Middlewares
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        private readonly IExceptionHandler _exceptionHandler;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger, IExceptionHandler exceptionHandler)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _exceptionHandler = exceptionHandler ?? throw new ArgumentNullException(nameof(exceptionHandler));
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);

                if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    var detail = "Request path was not found";
                    await HandleExceptionAsync(httpContext, HttpStatusCode.NotFound, detail);
                }
            }
            catch (Exception exception)
            {
                bool isValidationExceptionHandled = await _exceptionHandler.TryHandleAsync(httpContext, exception, CancellationToken.None);

                if (!isValidationExceptionHandled)
                {
                    HttpStatusCode httpStatusCode = exception switch
                    {
                        ArgumentNullException => HttpStatusCode.BadRequest,
                        UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                        HttpRequestException => HttpStatusCode.BadRequest,
                        _ => HttpStatusCode.InternalServerError,
                    };

                    var detail = exception.Message;

                    _logger.LogInformation($"Something went wrong: {detail}");

                    await HandleExceptionAsync(httpContext, httpStatusCode, detail);
                }
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, HttpStatusCode httpStatusCode, string detail)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)httpStatusCode;

            var problemDetails = new ProblemDetails
            {
                Type = "about:blank",
                Title = httpStatusCode.ToString(),
                Status = httpContext.Response.StatusCode,
                Detail = detail,
                Instance = httpContext.Request.Path
            };

            var serializedProblemDetails = JsonSerializer.Serialize(problemDetails);

            await httpContext.Response.WriteAsync(serializedProblemDetails);
        }
    }
}
