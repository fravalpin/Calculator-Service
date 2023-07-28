using CalculatorService.Server.Domain;
using FluentValidation;
using MediatR;

namespace CalculatorService.Server.Application.UsesCases
{
    public record AdditionRequest(double[]? Addends) : IRequest<double>;
    public class AdditionRequestHandler : IRequestHandler<AdditionRequest, double>
    {
        public AdditionRequestHandler()
        {
        }

        public Task<double> Handle(AdditionRequest request, CancellationToken cancellationToken)
        {
            Addition addition = new(request.Addends!);
            return Task.FromResult(addition.Calculate());
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
