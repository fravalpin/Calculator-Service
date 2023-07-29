using CalculatorService.Server.Domain;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CalculatorService.Server.Application.UsesCases
{
    public record SubtractionRequest(double? Minuend, double? Subtrahend) : IRequest<SubtractionResponse>;
    public record SubtractionResponse(double Difference);
    public class SubtractionRequestHandler : IRequestHandler<SubtractionRequest, SubtractionResponse>
    {
        private readonly ILogger<SubtractionRequestHandler> _logger;
        public SubtractionRequestHandler(ILogger<SubtractionRequestHandler> logger)
        {
            _logger = logger;
        }

        public Task<SubtractionResponse> Handle(SubtractionRequest request, CancellationToken cancellationToken)
        {
            if (request.Minuend == null || request.Subtrahend == null) throw new ArgumentNullException();

            _logger.LogDebug($"Calculating {request.Minuend} - {request.Subtrahend}");
            Subtraction subtraction = new(request.Minuend.Value, request.Subtrahend.Value);
            _logger.LogDebug($"Difference is {subtraction.Value}");
            return Task.FromResult(new SubtractionResponse(subtraction.Value));
        }
    }

    public class SubtractionValidator : AbstractValidator<SubtractionRequest>
    {
        private const string ErrorMessage = "The request should include at least two numeric operands to add";

        public SubtractionValidator()
        {
            RuleFor(p => p.Minuend)
                .NotEmpty() .WithMessage(ErrorMessage);
            RuleFor(p => p.Subtrahend)
                .NotEmpty() .WithMessage(ErrorMessage);
        }
    }

}
