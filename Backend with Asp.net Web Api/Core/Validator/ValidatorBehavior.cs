using Core.Features.Employee.Core.Bases;
using FluentValidation;
using MediatR;

namespace Core.Validator
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }



        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(
                    _validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Any())
                {
                    var errorMessages = failures
                        .Select(e => $"{e.PropertyName}: {e.ErrorMessage}")
                        .ToList();


                    var response = new Response<string>
                    {
                        Succeeded = false,
                        Message = "Validation failed",
                        Errors = errorMessages
                    };

                    return (TResponse)(object)response;
                }
            }
            return await next();
        }
    }
}
