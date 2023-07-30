
using CalculatorService.Server.Application.Abstractions;
using CalculatorService.Server.Application.UsesCases;
using CalculatorService.Server.Domain.Calculations;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using System.Runtime.Serialization;
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
            Mock<IJournalService> journal = new();
            AdditionRequestHandler requestHandler = new(logger.Object, journal.Object);
            AdditionResponse result = await requestHandler.Handle(additionRequest, CancellationToken.None);

            result.Should().NotBeNull();
            result.Sum.Should().Be(2);
        }

        [Fact]
        public async Task AdditionWithXEviTrackingId()
        {
            string trackingID = Guid.NewGuid().ToString();
            AdditionRequest additionRequest = new(new double[] { 1, 1 }, trackingID);
            Mock<ILogger<AdditionRequestHandler>> logger = new();
            Mock<IJournalService> journal = new();
            AdditionRequestHandler requestHandler = new(logger.Object, journal.Object);
            await requestHandler.Handle(additionRequest, CancellationToken.None);

            journal.Verify(x => x.Add(It.IsAny<ICalculation>(), trackingID), Times.Once());
        }

        [Fact]
        public async Task AdditionWithotXEviTrackingId()
        {
            AdditionRequest additionRequest = new(new double[] { 1, 1 });
            Mock<ILogger<AdditionRequestHandler>> logger = new();
            Mock<IJournalService> journal = new();
            AdditionRequestHandler requestHandler = new(logger.Object, journal.Object);
            await requestHandler.Handle(additionRequest, CancellationToken.None);

            journal.Verify(x => x.Add(It.IsAny<ICalculation>(), It.IsAny<string>()), Times.Never());
        }

    }
}
