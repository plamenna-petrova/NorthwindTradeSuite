using FluentValidation;
using MediatR;

namespace NorthwindTradeSuite.Application.Behaviors
{
    public class CustomValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator> _validators = null!;

        public CustomValidationBehavior(IEnumerable<IValidator> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var validationContext = new ValidationContext<TRequest>(request);

            var validationErrors = _validators
                .Select(validator => validator.Validate(validationContext))
                .SelectMany(validationResult => validationResult.Errors)
                .Where(validationError => validationError != null)
                .ToList();

            if (validationErrors.Any())
            {
                var joinedValidationErrorsMessage = string.Join("\r\n", validationErrors);
                throw new Exception(joinedValidationErrorsMessage);
            }

            return next();
        }
    }
}
