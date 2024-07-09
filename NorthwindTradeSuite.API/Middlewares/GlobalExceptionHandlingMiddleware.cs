using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using ValidationException = NorthwindTradeSuite.Application.Exceptions.ValidationException;

namespace NorthwindTradeSuite.API.Middlewares
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
                if (exception is ValidationException validationException)
                {
                    var fluentValidationProblemDetails = new ProblemDetails
                    {
                        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                        Title = "One or more validation errors occurred.",
                        Instance = httpContext.Request.Path
                    };

                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

                    fluentValidationProblemDetails.Extensions.Add("errors", validationException.ErrorsDictionary);
                    fluentValidationProblemDetails.Status = httpContext.Response.StatusCode;

                    _logger.LogError(exception, message: fluentValidationProblemDetails.Title);

                    await httpContext.Response.WriteAsJsonAsync(fluentValidationProblemDetails, CancellationToken.None).ConfigureAwait(false);
                }
                else
                {
                    HttpStatusCode httpStatusCode = exception switch
                    {
                        ArgumentNullException => HttpStatusCode.BadRequest,
                        UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                        HttpRequestException => HttpStatusCode.BadRequest,
                        KeyNotFoundException => HttpStatusCode.NotFound,
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
