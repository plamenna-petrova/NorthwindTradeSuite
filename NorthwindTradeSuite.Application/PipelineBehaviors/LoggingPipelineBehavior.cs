using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace NorthwindTradeSuite.Application.PipelineBehaviors
{
    public class LoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest
    {
        private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;

        public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));        
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var correlationId = Guid.NewGuid().ToString()[..7];
            var requestJSON = JsonSerializer.Serialize(request);

            _logger.LogInformation($"Handling request {typeof(TRequest).Name} with correlation ID: {correlationId}, {requestJSON}");

            var response = await next();
            var responseJSON = JsonSerializer.Serialize(response);

            _logger.LogInformation($"Response for correlation ID: {correlationId}, {responseJSON}");

            return response;
        }
    }
}
