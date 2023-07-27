using FluentAssertions;
using Xunit;

namespace CalculatorService.Server.Domain.UnitTests
{
    public class AdditionUnitTest
    {

        [Fact]
        public void AdditionNotOperands()
        {
            //setup
            double[] operands = null;
            Addition addition = new(operands);

            //act
            Action act = () => addition.Calculate();

            //asserts
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void AdditionJustOneOperand()
        {
            //setup
            double[] operands = new double[] { 1 };
            Addition addition = new(operands);

            //act
            Action act = () => addition.Calculate();

            //asserts
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void AdditionIntegers()
        {
            //setup
            double[] operands = new double[] { 1, 1 };
            Addition addition = new(operands);

            //act
            double result = addition.Calculate();

            //asserts
            result.Should().Be(2);
        }

        [Fact]
        public void AdditionDoubles()
        {
            //setup
            double[] operands = new double[] { 1.5, 1.5 };
            Addition addition = new(operands);

            //act
            double result = addition.Calculate();

            //asserts
            result.Should().Be(3);
        }

        [Fact]
        public void AdditionNegatives()
        {
            //setup
            double[] operands = new double[] { -1, -1 };
            Addition addition = new(operands);

            //act
            double result = addition.Calculate();

            //asserts
            result.Should().Be(-2);
        }

    }
}