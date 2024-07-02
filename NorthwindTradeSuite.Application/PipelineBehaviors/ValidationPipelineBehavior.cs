using FluentValidation;
using MediatR;
using NorthwindTradeSuite.Application.Contracts;

namespace NorthwindTradeSuite.Application.PipelineBehaviors
{
    public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>, IQuery<TResponse>
    {
        private readonly ICollection<IValidator> _validators = null!;

        public ValidationPipelineBehavior(ICollection<IValidator> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var validationContext = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(validationContext)));

            var validationFailures = validationResults
                .Where(vr => !vr.IsValid)
                .SelectMany(vr => vr.Errors)
                .Select(vr => vr.ErrorMessage);

            if (validationFailures.Any())
            {
                var joinedValidationErrorsMessage = string.Join("\r\n", validationFailures);
                throw new Exception(joinedValidationErrorsMessage);
            }

            return await next();
        }
    }
}
