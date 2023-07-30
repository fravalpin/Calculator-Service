using CalculatorService.Server.Application.Abstractions;
using CalculatorService.Server.Domain.Calculations;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CalculatorService.Server.Application.UsesCases
{
    public record AdditionBodyRequest(double[]? Addends);
    public record AdditionRequest(double[]? Addends, string? XEviTrackingID = null) : IRequest<AdditionResponse>;
    public record AdditionResponse(double Sum);
    public class AdditionRequestHandler : IRequestHandler<AdditionRequest, AdditionResponse>
    {
        private readonly ILogger<AdditionRequestHandler> _logger;
        private readonly IJournalService _journalService;

        public AdditionRequestHandler(ILogger<AdditionRequestHandler> logger, IJournalService journalService)
        {
            _logger = logger;
            _journalService = journalService;
        }

        public Task<AdditionResponse> Handle(AdditionRequest request, CancellationToken cancellationToken)
        {
            if (request?.Addends == null) throw new ArgumentNullException();

            Addition addition = new(request.Addends!);
            _logger.LogDebug($"Calculated {addition}");

            if (!string.IsNullOrEmpty(request.XEviTrackingID))
                _journalService.Add(addition, request.XEviTrackingID);

            return Task.FromResult(new AdditionResponse(addition.Value));
        }
    }

    public class AdditionValidator : AbstractValidator<AdditionRequest>
    {
        public AdditionValidator()
        {
            RuleFor(p => p.Addends)
                .Must(p => p != null && p.Length > 1)
                .WithMessage("The request should include at least two numeric operands to add");
        }
    }

}
