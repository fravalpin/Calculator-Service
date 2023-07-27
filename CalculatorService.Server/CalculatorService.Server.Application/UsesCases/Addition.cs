using CalculatorService.Server.Domain;
using FluentValidation;
using MediatR;

namespace CalculatorService.Server.Application.UsesCases
{
    public record AdditionRequest(double[]? Operands) : IRequest<double>;
    public class AdditionRequestHandler : IRequestHandler<AdditionRequest, double>
    {
        public AdditionRequestHandler()
        {
        }

        public Task<double> Handle(AdditionRequest request, CancellationToken cancellationToken)
        {
            Addition addition = new(request.Operands!);
            return Task.FromResult(addition.Calculate());
        }
    }

    public class AdditionValidator : AbstractValidator<AdditionRequest>
    {
        public AdditionValidator()
        {
            RuleFor(p => p.Operands)
                .Must(p => p != null && p.Length > 1)
                .WithMessage("The request should include at least two numeric operands to add");
        }
    }

}
