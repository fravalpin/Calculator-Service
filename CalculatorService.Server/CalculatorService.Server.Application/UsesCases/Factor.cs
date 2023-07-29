using CalculatorService.Server.Domain;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CalculatorService.Server.Application.UsesCases
{
    public record FactorRequest(double[]? Factors) : IRequest<FactorResponse>;
    public record FactorResponse(double Product);
    public class FactorRequestHandler : IRequestHandler<FactorRequest, FactorResponse>
    {
        private readonly ILogger<FactorRequestHandler> _logger;
        public FactorRequestHandler(ILogger<FactorRequestHandler> logger)
        {
            _logger = logger;
        }

        public Task<FactorResponse> Handle(FactorRequest request, CancellationToken cancellationToken)
        {
            if (request.Factors == null) throw new ArgumentNullException();

            _logger.LogDebug($"Calculating {string.Join(" x ", request.Factors)}");
            Factor factor = new(request.Factors!);
            _logger.LogDebug($"Product is {factor.Value}");
            return Task.FromResult(new FactorResponse(factor.Value));
        }
    }

    public class FactorValidator : AbstractValidator<FactorRequest>
    {
        public FactorValidator()
        {
            RuleFor(p => p.Factors)
                .Must(p => p != null && p.Length > 1)
                .WithMessage("The request should include at least two numeric operands to add");
        }
    }

}
