
using CalculatorService.Server.Application.UsesCases;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CalculatorService.Server.Application.UnitTests
{
    public class DivisionUnitTest
    {
        [Fact]
        public void DivisionValidationNull()
        {
            DivisionValidator validator = new();
            DivisionRequest divisionRequest = new(null, null);
            Action act = () => validator.ValidateAndThrow(divisionRequest);

            act.Should().Throw<ValidationException>();
        }

        [Fact]
        public void DivisionValidationEmptyFirstParam()
        {
            DivisionValidator validator = new();
            DivisionRequest divisionRequest = new(null, 0);
            Action act = () => validator.ValidateAndThrow(divisionRequest);

            act.Should().Throw<ValidationException>();
        }

        [Fact]
        public void DivisionValidationEmptySecondParam()
        {
            DivisionValidator validator = new();
            DivisionRequest divisionRequest = new(0, null);
            Action act = () => validator.ValidateAndThrow(divisionRequest);

            act.Should().Throw<ValidationException>();
        }

        [Fact]
        public void DivisionValidationGood()
        {
            DivisionValidator validator = new();
            DivisionRequest divisionRequest = new(2, 2);
            Action act = () => validator.ValidateAndThrow(divisionRequest);

            act.Should().NotThrow<ValidationException>();
        }

        [Fact]
        public async Task DivisionGetResult()
        {
            DivisionRequest divisionRequest = new(8, 4);
            Mock<ILogger<DivisionRequestHandler>> logger = new();
            DivisionRequestHandler requestHandler = new(logger.Object);
            DivisionResponse result = await requestHandler.Handle(divisionRequest, CancellationToken.None);

            result.Quotient.Should().Be(2);
            result.Remainder.Should().Be(0);
        }

    }
}
