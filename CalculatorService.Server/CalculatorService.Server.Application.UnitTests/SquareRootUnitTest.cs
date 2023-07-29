
using CalculatorService.Server.Application.UsesCases;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CalculatorService.Server.Application.UnitTests
{
    public class SquareRootUnitTest
    {
        [Fact]
        public void SquareRootValidationNull()
        {
            SquareRootValidator validator = new();
            SquareRootRequest squareRootRequest = new(null);
            Action act = () => validator.ValidateAndThrow(squareRootRequest);

            act.Should().Throw<ValidationException>();
        }

        [Fact]
        public void SquareRootValidationGood()
        {
            SquareRootValidator validator = new();
            SquareRootRequest squareRootRequest = new(2);
            Action act = () => validator.ValidateAndThrow(squareRootRequest);

            act.Should().NotThrow<ValidationException>();
        }

        [Fact]
        public async Task SquareRootGetResult()
        {
            SquareRootRequest squareRootRequest = new(16);
            Mock<ILogger<SquareRootRequestHandler>> logger = new();
            SquareRootRequestHandler requestHandler = new(logger.Object);
            SquareRootResponse result = await requestHandler.Handle(squareRootRequest, CancellationToken.None);

            result.Square.Should().Be(4);
        }

    }
}
