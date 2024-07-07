using FluentValidation;
using MediatR;
using ValidationException = NorthwindTradeSuite.Application.Exceptions.ValidationException;

namespace NorthwindTradeSuite.Application.PipelineBehaviors
{
    public sealed class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
       where TRequest : class, IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
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

                var validationFailuresDictionary = validationResults
                    .SelectMany(vr => vr.Errors)
                    .Where(vf => vf != null)
                    .GroupBy(
                        vf => vf.PropertyName.Split(".").Last(),
                        vf => vf.ErrorMessage,
                        (propertyName, errorMessages) => new
                        {
                            Key = propertyName,
                            Values = errorMessages.Distinct().ToArray()
                        })
                    .ToDictionary(x => x.Key, x => x.Values);

                if (validationFailuresDictionary.Any())
                {
                    throw new ValidationException(validationFailuresDictionary);
                }

            }

            return await next().ConfigureAwait(false);
        }
    }
}
