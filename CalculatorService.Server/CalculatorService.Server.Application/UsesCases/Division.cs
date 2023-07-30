using CalculatorService.Server.Application.Abstractions;
using CalculatorService.Server.Domain.Calculations;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CalculatorService.Server.Application.UsesCases
{
    public record DivisionBodyRequest(double? Dividend, double? Divisor);
    public record DivisionRequest(double? Dividend, double? Divisor, string? XEviTrackingID = null) : IRequest<DivisionResponse>;
    public record DivisionResponse(double Quotient, double Remainder);
    public class DivisionRequestHandler : IRequestHandler<DivisionRequest, DivisionResponse>
    {
        private readonly ILogger<DivisionRequestHandler> _logger;
        private readonly IJournalService _journalService;

        public DivisionRequestHandler(ILogger<DivisionRequestHandler> logger, IJournalService journalService)
        {
            _logger = logger;
            _journalService = journalService;
        }

        public Task<DivisionResponse> Handle(DivisionRequest request, CancellationToken cancellationToken)
        {
            if (request.Dividend == null || request.Divisor == null) throw new ArgumentNullException();

            Division division = new(request.Dividend.Value, request.Divisor.Value);

            _logger.LogDebug($"Calculated {division}");
            if (!string.IsNullOrEmpty(request.XEviTrackingID))
                _journalService.Add(division, request.XEviTrackingID);
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
