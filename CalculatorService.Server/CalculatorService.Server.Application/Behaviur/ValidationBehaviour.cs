using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CalculatorService.Server.Application.Behaviour
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger<ValidationBehaviour<TRequest, TResponse>> _logger;
        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationBehaviour<TRequest, TResponse>> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                if (_validators.Any())
                {
                    string? message = $"Validation failed for request {request} with message: ";

                    var context = new ValidationContext<TRequest>(request);

                    var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                    var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                    if (failures.Count() == 1)
                    {
                        message += failures.FirstOrDefault()?.ErrorMessage;
                        ThrowException(_logger, message, new ValidationException(message, failures, false));
                    }

                    if (failures.Count() > 0)
                    {
                        message += string.Join(", ", failures.Select(x => x.ErrorMessage));
                        ThrowException(_logger, message, new ValidationException(failures));
                    }
                }
                return await next();
            }
            catch (Exception ex)
            {
                if(ex is not ValidationException)
                    _logger.LogError(ex, $"Exception checking validations");
                throw;
            }
        }

        private static void ThrowException(ILogger<ValidationBehaviour<TRequest, TResponse>> logger, string message, ValidationException exception)
        {
            logger.LogWarning(message);
            throw exception;
        }
    }
}
