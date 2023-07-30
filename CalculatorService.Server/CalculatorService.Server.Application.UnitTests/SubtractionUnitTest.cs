
using CalculatorService.Server.Application.Abstractions;
using CalculatorService.Server.Application.UsesCases;
using CalculatorService.Server.Domain.Calculations;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CalculatorService.Server.Application.UnitTests
{
    public class SubtractionUnitTest
    {
        [Fact]
        public void SubtractionValidationNull()
        {
            SubtractionValidator validator = new();
            SubtractionRequest subtractionRequest = new(null, null);
            Action act = () => validator.ValidateAndThrow(subtractionRequest);

            act.Should().Throw<ValidationException>();
        }

        [Fact]
        public void SubtractionValidationEmptyFirstParam()
        {
            SubtractionValidator validator = new();
            SubtractionRequest subtractionRequest = new(null, 0);
            Action act = () => validator.ValidateAndThrow(subtractionRequest);

            act.Should().Throw<ValidationException>();
        }

        [Fact]
        public void SubtractionValidationEmptySecondParam()
        {
            SubtractionValidator validator = new();
            SubtractionRequest subtractionRequest = new(0, null);
            Action act = () => validator.ValidateAndThrow(subtractionRequest);

            act.Should().Throw<ValidationException>();
        }

        [Fact]
        public void SubtractionValidationGood()
        {
            SubtractionValidator validator = new();
            SubtractionRequest subtractionRequest = new(2, 2);
            Action act = () => validator.ValidateAndThrow(subtractionRequest);

            act.Should().NotThrow<ValidationException>();
        }

        [Fact]
        public async Task SubtractionGetResult()
        {
            SubtractionRequest subtractionRequest = new(8,-4);
            Mock<ILogger<SubtractionRequestHandler>> logger = new();
            Mock<IJournalService> journal = new();
            SubtractionRequestHandler requestHandler = new(logger.Object, journal.Object);
            SubtractionResponse result = await requestHandler.Handle(subtractionRequest, CancellationToken.None);

            result.Should().NotBeNull();
            result.Difference.Should().Be(4);
        }

        [Fact]
        public async Task SubtractionWithXEviTrackingId()
        {
            string trackingID = Guid.NewGuid().ToString();
            SubtractionRequest subtractionRequest = new(5, 2, trackingID);
            Mock<ILogger<SubtractionRequestHandler>> logger = new();
            Mock<IJournalService> journal = new();
            SubtractionRequestHandler requestHandler = new(logger.Object, journal.Object);
            await requestHandler.Handle(subtractionRequest, CancellationToken.None);

            journal.Verify(x => x.Add(It.IsAny<ICalculation>(), trackingID), Times.Once());
        }

        [Fact]
        public async Task SubtractionWithotXEviTrackingId()
        {
            SubtractionRequest subtractionRequest = new(5, 2);
            Mock<ILogger<SubtractionRequestHandler>> logger = new();
            Mock<IJournalService> journal = new();
            SubtractionRequestHandler requestHandler = new(logger.Object, journal.Object);
            await requestHandler.Handle(subtractionRequest, CancellationToken.None);

            journal.Verify(x => x.Add(It.IsAny<ICalculation>(), It.IsAny<string>()), Times.Never());
        }

    }
}
