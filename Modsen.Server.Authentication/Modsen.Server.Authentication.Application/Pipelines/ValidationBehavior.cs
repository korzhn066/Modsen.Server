using FluentValidation;
using MediatR;
using Modsen.Server.Shared.Exceptions;

namespace Modsen.Server.Authentication.Application.Pipelines
{
    public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : class
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var validationFailures = await Task.WhenAll(
                _validators.Select(validator => validator.ValidateAsync(context)));

            var errors = validationFailures
                .Where(validationResult => !validationResult.IsValid)
                .SelectMany(validationResult => validationResult.Errors)
                .Select(validationFailure => validationFailure.ErrorMessage)
                .ToList();

            if (errors.Count != 0)
            {
                throw new BadRequestException(string.Join(' ', errors));
            }

            return await next();
        }
    }
}