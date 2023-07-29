using CalculatorService.Server.Application.Behaviour;
using CalculatorService.Server.Application.UsesCases;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CalculatorService.Server.Application.UnitTests.Behaviur
{
    public class ValidationBehaviourUnitTest
    {

        [Fact]
        public async Task ValidationBehaviourExceptionTest()
        {
            Mock<RequestHandlerDelegate<AdditionResponse>> requestDelegate = new();
            ValidationBehaviour<AdditionRequest, AdditionResponse> validationBehaviour = new(null,null);
            AdditionRequest additionRequest = new(new double[] { 0 });
            Func<Task<AdditionResponse>> act = () => validationBehaviour.Handle(additionRequest, CancellationToken.None, requestDelegate.Object);

            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task ValidationBehaviourNoValidations()
        {
            Mock<RequestHandlerDelegate<AdditionResponse>> requestDelegate = new();
            ValidationResult validationResult = new();

            List<IValidator<AdditionRequest>> validators = new();
            Mock<ILogger<ValidationBehaviour<AdditionRequest, AdditionResponse>>> logger = new();

            ValidationBehaviour<AdditionRequest, AdditionResponse> validationBehaviour = new(validators, logger.Object);
            AdditionRequest additionRequest = new(new double[] { 0 });
            Func<Task<AdditionResponse>> act = () => validationBehaviour.Handle(additionRequest, CancellationToken.None, requestDelegate.Object);

            await act.Should().NotThrowAsync();
        }

        [Fact]
        public async Task ValidationBehaviourValidationsWithoutErrorsTest()
        {
            Mock<RequestHandlerDelegate<AdditionResponse>> requestDelegate = new();
            Mock<IValidator<AdditionRequest>> validations = new();
            ValidationResult validationResult = new();

            validations.Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<AdditionRequest>>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationResult);

            List<IValidator<AdditionRequest>> validators = new()
            {
                validations.Object
            };
            Mock<ILogger<ValidationBehaviour<AdditionRequest, AdditionResponse>>> logger = new();

            ValidationBehaviour<AdditionRequest, AdditionResponse> validationBehaviour = new(validators, logger.Object);
            AdditionRequest additionRequest = new(new double[] { 0 });
            Func<Task<AdditionResponse>> act = () => validationBehaviour.Handle(additionRequest, CancellationToken.None, requestDelegate.Object);

            await act.Should().NotThrowAsync();
        }

        [Fact]
        public async Task ValidationBehaviourValidationsWithOneErrorTest()
        {
            Mock<RequestHandlerDelegate<AdditionResponse>> requestDelegate = new();
            Mock<IValidator<AdditionRequest>> validations = new();

            ValidationResult validationResult = new();
            validationResult.Errors.Add(new ValidationFailure("Property", "Error"));
            validations.Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<AdditionRequest>>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationResult);


            List<IValidator<AdditionRequest>> validators = new()
            {
                validations.Object
            };
            Mock<ILogger<ValidationBehaviour<AdditionRequest, AdditionResponse>>> logger = new();

            ValidationBehaviour<AdditionRequest, AdditionResponse> validationBehaviour = new(validators, logger.Object);
            AdditionRequest additionRequest = new(new double[] { 0 });
            Func<Task<AdditionResponse>> act = () => validationBehaviour.Handle(additionRequest, CancellationToken.None, requestDelegate.Object);


            await act.Should().ThrowAsync<ValidationException>();
            logger.Verify(logger => logger.Log(
                It.Is<LogLevel>(l => l == LogLevel.Warning),
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task ValidationBehaviourValidationsWithMoreThanOneErrorTest()
        {
            Mock<RequestHandlerDelegate<AdditionResponse>> requestDelegate = new();
            Mock<IValidator<AdditionRequest>> validations = new();

            ValidationResult validationResult = new();
            validationResult.Errors.Add(new ValidationFailure("Property", "Error"));
            validationResult.Errors.Add(new ValidationFailure("Property2", "Error2"));
            validations.Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<AdditionRequest>>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationResult);


            List<IValidator<AdditionRequest>> validators = new()
            {
                validations.Object
            };
            Mock<ILogger<ValidationBehaviour<AdditionRequest, AdditionResponse>>> logger = new();

            ValidationBehaviour<AdditionRequest, AdditionResponse> validationBehaviour = new(validators, logger.Object);
            AdditionRequest additionRequest = new(new double[] { 0 });
            Func<Task<AdditionResponse>> act = () => validationBehaviour.Handle(additionRequest, CancellationToken.None, requestDelegate.Object);

            await act.Should().ThrowAsync<ValidationException>();
            logger.Verify(logger => logger.Log(
                It.Is<LogLevel>(l => l == LogLevel.Warning),
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }

    }
}
