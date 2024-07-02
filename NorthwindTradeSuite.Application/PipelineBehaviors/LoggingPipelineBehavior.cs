using MediatR;
using Microsoft.Extensions.Logging;

namespace NorthwindTradeSuite.Application.PipelineBehaviors
{
    public class LoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest
    {
        private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger = null!;

        public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;        
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            string requestName = typeof(TRequest).Name;

            _logger.LogInformation($"Processing request {requestName}");

            return await next();
        }
    }
}
