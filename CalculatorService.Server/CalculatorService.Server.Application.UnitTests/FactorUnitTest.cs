
using CalculatorService.Server.Application.UsesCases;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CalculatorService.Server.Application.UnitTests
{
    public class FactorUnitTest
    {
        [Fact]
        public void FactorValidationNull()
        {
            FactorValidator validator = new();
            FactorRequest factorRequest = new(null);
            Action act = () => validator.ValidateAndThrow(factorRequest);

            act.Should().Throw<ValidationException>();
        }

        [Fact]
        public void FactorValidationEmpty()
        {
            FactorValidator validator = new();
            FactorRequest factorRequest = new(new double[] { });
            Action act = () => validator.ValidateAndThrow(factorRequest);

            act.Should().Throw<ValidationException>();
        }

        [Fact]
        public void FactorValidationOnlyOneOperand()
        {
            FactorValidator validator = new();
            FactorRequest factorRequest = new(new double[] { 1 });
            Action act = () => validator.ValidateAndThrow(factorRequest);

            act.Should().Throw<ValidationException>();
        }

        [Fact]
        public void FactorValidationGood()
        {
            FactorValidator validator = new();
            FactorRequest factorRequest = new(new double[] { 1 , 1 });
            Action act = () => validator.ValidateAndThrow(factorRequest);

            act.Should().NotThrow<ValidationException>();
        }

        [Fact]
        public async Task FactorGetResult()
        {
            FactorRequest factorRequest = new(new double[] { 2, 8 });
            Mock<ILogger<FactorRequestHandler>> logger = new();
            FactorRequestHandler requestHandler = new(logger.Object);
            FactorResponse result = await requestHandler.Handle(factorRequest, CancellationToken.None);

            result.Should().NotBeNull();
            result.Product.Should().Be(16);
        }

    }
}
