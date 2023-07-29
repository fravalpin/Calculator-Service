using CalculatorService.Server.Domain;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CalculatorService.Server.Application.UsesCases
{
    public record AdditionBodyRequest(double[]? Addends);
    public record AdditionRequest(double[]? Addends, string? XEviTrackingId) : IRequest<AdditionResponse>;
    public record AdditionResponse(double Sum);
    public class AdditionRequestHandler : IRequestHandler<AdditionRequest, AdditionResponse>
    {
        private readonly ILogger<AdditionRequestHandler> _logger;

        public AdditionRequestHandler(ILogger<AdditionRequestHandler> logger)
        {
            _logger = logger;
        }

        public Task<AdditionResponse> Handle(AdditionRequest request, CancellationToken cancellationToken)
        {
            if (request?.Addends == null) throw new ArgumentNullException();

            _logger.LogDebug($"Calculating {string.Join(" + ", request.Addends)}");
            Addition addition = new(request.Addends!);
            _logger.LogDebug($"Sum is {addition.Value}");
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
