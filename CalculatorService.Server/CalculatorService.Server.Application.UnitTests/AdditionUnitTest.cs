
using CalculatorService.Server.Application.UsesCases;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CalculatorService.Server.Application.UnitTests
{
    public class AdditionUnitTest
    {
        [Fact]
        public void AdditionValidationNull()
        {
            AdditionValidator validator = new();
            AdditionRequest additionRequest = new(null);
            Action act = () => validator.ValidateAndThrow(additionRequest);

            act.Should().Throw<ValidationException>();
        }

        [Fact]
        public void AdditionValidationEmpty()
        {
            AdditionValidator validator = new();
            AdditionRequest additionRequest = new(new double[] { });
            Action act = () => validator.ValidateAndThrow(additionRequest);

            act.Should().Throw<ValidationException>();
        }

        [Fact]
        public void AdditionValidationOnlyOneOperand()
        {
            AdditionValidator validator = new();
            AdditionRequest additionRequest = new(new double[] { 1 });
            Action act = () => validator.ValidateAndThrow(additionRequest);

            act.Should().Throw<ValidationException>();
        }

        [Fact]
        public void AdditionValidationGood()
        {
            AdditionValidator validator = new();
            AdditionRequest additionRequest = new(new double[] { 1 , 1 });
            Action act = () => validator.ValidateAndThrow(additionRequest);

            act.Should().NotThrow<ValidationException>();
        }

        [Fact]
        public async Task AdditionGetResult()
        {
            AdditionRequest additionRequest = new(new double[] { 1, 1 });
            Mock<ILogger<AdditionRequestHandler>> logger = new();
            AdditionRequestHandler requestHandler = new(logger.Object);
            AdditionResponse result = await requestHandler.Handle(additionRequest, CancellationToken.None);

            result.Should().NotBeNull();
            result.Sum.Should().Be(2);
        }

    }
}
