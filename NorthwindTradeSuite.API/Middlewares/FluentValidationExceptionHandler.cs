using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NorthwindTradeSuite.API.Middlewares.Contracts;

namespace NorthwindTradeSuite.API.Middlewares
{
    public class FluentValidationExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<FluentValidationExceptionHandler> _logger;

        public FluentValidationExceptionHandler(ILogger<FluentValidationExceptionHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var problemDetails = new ProblemDetails
            {
                Instance = httpContext.Request.Path
            };

            if (exception is ValidationException fluentValidationException)
            {
                problemDetails.Title = "One or more validation errors occurred.";
                problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

                List<string> fluentValidationErrorsMessages = new();

                foreach (var fluentValidationError in fluentValidationException.Errors)
                {
                    fluentValidationErrorsMessages.Add(fluentValidationError.ErrorMessage);
                }

                problemDetails.Extensions.Add("errors", fluentValidationErrorsMessages);

                problemDetails.Status = httpContext.Response.StatusCode;

                _logger.LogError(exception, problemDetails.Title);

                await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken).ConfigureAwait(false);

                return true;
            }

            return false;
        }
    }
}
