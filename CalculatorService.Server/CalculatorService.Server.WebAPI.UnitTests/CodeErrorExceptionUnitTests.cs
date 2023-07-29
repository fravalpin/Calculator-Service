using CalculatorService.Server.WebAPI.Middleware.ResponseException.ResponseException;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace CalculatorService.Server.WebAPI.UnitTests
{
    public class CodeErrorExceptionUnitTests
    {

        [Fact]
        public void FluentValidationException()
        {
            //setup
            string message = "Test";
            ValidationException validationException = new(message);

            //act
            ResponseValidationException codeErrorException = new(validationException);

            //asserts
            codeErrorException.ErrorStatus.Should().Be(400);
            codeErrorException.ErrorCode.Should().Be("InternalError");
            codeErrorException.ErrorMessage.Should().Be("Unable to process request: " + message);
        }

        [Fact]
        public void GenericException()
        {
            //act
            ResponseGeneralException codeErrorException = new();

            //asserts
            codeErrorException.ErrorStatus.Should().Be(500);
            codeErrorException.ErrorCode.Should().Be("InternalError");
            codeErrorException.ErrorMessage.Should().Be("An unexpected error condition was triggered which made impossible to fulfill the request. Please try again or contact support.");
        }
    }
}