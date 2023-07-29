using CalculatorService.Server.Domain;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CalculatorService.Server.Application.UsesCases
{
    public record DivisionRequest(double? Dividend, double? Divisor) : IRequest<DivisionResponse>;
    public record DivisionResponse(double Quotient, double Remainder);
    public class DivisionRequestHandler : IRequestHandler<DivisionRequest, DivisionResponse>
    {
        private readonly ILogger<DivisionRequestHandler> _logger;
        public DivisionRequestHandler(ILogger<DivisionRequestHandler> logger)
        {
            _logger = logger;
        }

        public Task<DivisionResponse> Handle(DivisionRequest request, CancellationToken cancellationToken)
        {
            if (request.Dividend == null || request.Divisor == null) throw new ArgumentNullException();

            _logger.LogDebug($"Calculating {request.Dividend} / {request.Divisor}");
            Division division = new(request.Dividend.Value, request.Divisor.Value);
            _logger.LogDebug($"Quotient is {division.Quotient} and Remainder is {division.Remainder}");
            return Task.FromResult(new DivisionResponse(division.Quotient, division.Remainder));
        }
    }

    public class DivisionValidator : AbstractValidator<DivisionRequest>
    {
        private const string ErrorMessage = "The request should include at least two numeric operands to add";

        public DivisionValidator()
        {
            RuleFor(p => p.Dividend)
                .NotEmpty() .WithMessage(ErrorMessage);
            RuleFor(p => p.Divisor)
                .NotEmpty().WithMessage(ErrorMessage)
                .NotEqual(0).WithMessage("Divisor can't be zero");
        }
    }

}
