
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
            Mock<IJournalService> journal = new();
            SquareRootRequestHandler requestHandler = new(logger.Object, journal.Object);
            SquareRootResponse result = await requestHandler.Handle(squareRootRequest, CancellationToken.None);

            result.Square.Should().Be(4);
        }
        
        [Fact]
        public async Task SquareRootWithXEviTrackingId()
        {
            string trackingID = Guid.NewGuid().ToString();
            SquareRootRequest squareRootRequest = new(16, trackingID);
            Mock<ILogger<SquareRootRequestHandler>> logger = new();
            Mock<IJournalService> journal = new();
            SquareRootRequestHandler requestHandler = new(logger.Object, journal.Object);
            await requestHandler.Handle(squareRootRequest, CancellationToken.None);

            journal.Verify(x => x.Add(It.IsAny<ICalculation>(), trackingID), Times.Once());
        }

        [Fact]
        public async Task SquareRootWithotXEviTrackingId()
        {
            SquareRootRequest squareRootRequest = new(16);
            Mock<ILogger<SquareRootRequestHandler>> logger = new();
            Mock<IJournalService> journal = new();
            SquareRootRequestHandler requestHandler = new(logger.Object, journal.Object);
            await requestHandler.Handle(squareRootRequest, CancellationToken.None);

            journal.Verify(x => x.Add(It.IsAny<ICalculation>(), It.IsAny<string>()), Times.Never());
        }

    }
}
