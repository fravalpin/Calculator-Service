using CalculatorService.Server.Domain;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CalculatorService.Server.Application.UsesCases
{
    public record SquareRootRequest(double? Number) : IRequest<SquareRootResponse>;
    public record SquareRootResponse(double Square);
    public class SquareRootRequestHandler : IRequestHandler<SquareRootRequest, SquareRootResponse>
    {
        private readonly ILogger<SquareRootRequestHandler> _logger;
        public SquareRootRequestHandler(ILogger<SquareRootRequestHandler> logger)
        {
            _logger = logger;
        }

        public Task<SquareRootResponse> Handle(SquareRootRequest request, CancellationToken cancellationToken)
        {
            if (request.Number == null) throw new ArgumentNullException();

            _logger.LogDebug($"Calculating sqrt({request.Number})");
            SquareRoot squareRoot = new(request.Number.Value);
            _logger.LogDebug($"Square is {squareRoot.Value}");
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
