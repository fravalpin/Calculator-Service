using CalculatorService.Server.Domain;
using FluentValidation;
using MediatR;

namespace CalculatorService.Server.Application.UsesCases
{
    public record SquareRootRequest(double? Number) : IRequest<SquareRootResponse>;
    public record SquareRootResponse(double Square);
    public class SquareRootRequestHandler : IRequestHandler<SquareRootRequest, SquareRootResponse>
    {
        public SquareRootRequestHandler()
        {
        }

        public Task<SquareRootResponse> Handle(SquareRootRequest request, CancellationToken cancellationToken)
        {
            if (request.Number == null) throw new ArgumentNullException();

            SquareRoot squareRoot = new(request.Number.Value);
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
