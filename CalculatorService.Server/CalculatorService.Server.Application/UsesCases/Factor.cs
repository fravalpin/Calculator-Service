using CalculatorService.Server.Domain;
using FluentValidation;
using MediatR;

namespace CalculatorService.Server.Application.UsesCases
{
    public record FactorRequest(double[]? Factors) : IRequest<FactorResponse>;
    public record FactorResponse(double Product);
    public class FactorRequestHandler : IRequestHandler<FactorRequest, FactorResponse>
    {
        public FactorRequestHandler()
        {
        }

        public Task<FactorResponse> Handle(FactorRequest request, CancellationToken cancellationToken)
        {
            if (request.Factors == null) throw new ArgumentNullException();

            Factor addition = new(request.Factors!);
            return Task.FromResult(new FactorResponse(addition.Calculate()));
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
