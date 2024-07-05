using FluentValidation;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using NorthwindTradeSuite.Application.Contracts;

namespace NorthwindTradeSuite.Application.PipelineBehaviors
{
    public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : class
    {
        private readonly IEnumerable<IValidator> _validators;
                                                                                
        public ValidationPipelineBehavior(IEnumerable<IValidator> validators)
        {
            _validators = validators ?? throw new ArgumentNullException(nameof(validators));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            if (_validators.Any())
            {
                var validationContext = new ValidationContext<TRequest>(request);

                var validationResults = await Task
                    .WhenAll(_validators.Select(v => v.ValidateAsync(validationContext)))
                    .ConfigureAwait(false);

                var validationFailures = validationResults
                    .Where(vr => !vr.IsValid)
                    .SelectMany(vr => vr.Errors)
                    .Select(vr => vr.ErrorMessage)
                    .ToList();

                if (validationFailures.Count > 0)
                {
                    var joinedValidationErrorsMessage = string.Join("\r\n", validationFailures);
                    throw new ValidationException(joinedValidationErrorsMessage);
                }
            }

            return await next().ConfigureAwait(false);
        }
    }
}
