using CalculatorService.Server.Application.Abstractions;
using CalculatorService.Server.Domain.Calculations;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CalculatorService.Server.Application.UsesCases
{
    public record SquareRootBodyRequest(double? Number);
    public record SquareRootRequest(double? Number, string? XEviTrackingID = null) : IRequest<SquareRootResponse>;
    public record SquareRootResponse(double Square);
    public class SquareRootRequestHandler : IRequestHandler<SquareRootRequest, SquareRootResponse>
    {
        private readonly ILogger<SquareRootRequestHandler> _logger;
        private readonly IJournalService _journalService;
        public SquareRootRequestHandler(ILogger<SquareRootRequestHandler> logger, IJournalService journalService)
        {
            _logger = logger;
            _journalService = journalService;
        }

        public Task<SquareRootResponse> Handle(SquareRootRequest request, CancellationToken cancellationToken)
        {
            if (request.Number == null) throw new ArgumentNullException();

            SquareRoot squareRoot = new(request.Number.Value);
            _logger.LogDebug($"Calculated {squareRoot}");

            if (!string.IsNullOrEmpty(request.XEviTrackingID))
                _journalService.Add(squareRoot, request.XEviTrackingID);
            return Task.FromResult(new SquareRootResponse(squareRoot.Value));
        }
    }

    public class SquareRootValidator : AbstractValidator<SquareRootRequest>
    {
        public SquareRootValidator()
        {
            RuleFor(p => p.Number)
                .NotEmpty()
                .GreaterThanOrEqualTo(0)
                .WithMessage("Number must be greater than or equal to zero");
        }
    }

}
