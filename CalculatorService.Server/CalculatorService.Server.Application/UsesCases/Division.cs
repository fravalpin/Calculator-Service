using CalculatorService.Server.Domain;
using FluentValidation;
using MediatR;

namespace CalculatorService.Server.Application.UsesCases
{
    public record DivisionRequest(double? Dividend, double? Divisor) : IRequest<DivisionResponse>;
    public record DivisionResponse(double Quotient, double Remainder);
    public class DivisionRequestHandler : IRequestHandler<DivisionRequest, DivisionResponse>
    {
        public DivisionRequestHandler()
        {
        }

        public Task<DivisionResponse> Handle(DivisionRequest request, CancellationToken cancellationToken)
        {
            if (request.Dividend == null || request.Divisor == null) throw new ArgumentNullException();

            Division division = new(request.Dividend.Value, request.Divisor.Value);
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
                .NotEqual(0) .WithMessage("Divisor can't be zero");
        }
    }

}
