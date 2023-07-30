using CalculatorService.Server.Application.Abstractions;
using CalculatorService.Server.Domain.Calculations;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CalculatorService.Server.Application.UsesCases
{
    public record FactorBodyRequest(double[]? Factors);
    public record FactorRequest(double[]? Factors, string? XEviTrackingID = null) : IRequest<FactorResponse>;
    public record FactorResponse(double Product);
    public class FactorRequestHandler : IRequestHandler<FactorRequest, FactorResponse>
    {
        private readonly ILogger<FactorRequestHandler> _logger;
        private readonly IJournalService _journalService;
        public FactorRequestHandler(ILogger<FactorRequestHandler> logger, IJournalService journalService)
        {
            _logger = logger;
            _journalService = journalService;
        }

        public Task<FactorResponse> Handle(FactorRequest request, CancellationToken cancellationToken)
        {
            if (request.Factors == null) throw new ArgumentNullException();

            Factor factor = new(request.Factors!);
            _logger.LogDebug($"Calculated {factor}");

            if (!string.IsNullOrEmpty(request.XEviTrackingID))
                _journalService.Add(factor, request.XEviTrackingID);
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
