using CalculatorService.Server.Application.Abstractions;
using CalculatorService.Server.Domain.Calculations;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CalculatorService.Server.Application.UsesCases
{
    public record SubtractionBodyRequest(double? Minuend, double? Subtrahend);
    public record SubtractionRequest(double? Minuend, double? Subtrahend, string? XEviTrackingID = null) : IRequest<SubtractionResponse>;
    public record SubtractionResponse(double Difference);
    public class SubtractionRequestHandler : IRequestHandler<SubtractionRequest, SubtractionResponse>
    {
        private readonly ILogger<SubtractionRequestHandler> _logger;
        private readonly IJournalService _journalService;
        public SubtractionRequestHandler(ILogger<SubtractionRequestHandler> logger, IJournalService journalService)
        {
            _logger = logger;
            _journalService = journalService;
        }

        public Task<SubtractionResponse> Handle(SubtractionRequest request, CancellationToken cancellationToken)
        {
            if (request.Minuend == null || request.Subtrahend == null) throw new ArgumentNullException();

            Subtraction subtraction = new(request.Minuend.Value, request.Subtrahend.Value);
            _logger.LogDebug($"Calculated {subtraction}");

            if (!string.IsNullOrEmpty(request.XEviTrackingID))
                _journalService.Add(subtraction, request.XEviTrackingID);
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
