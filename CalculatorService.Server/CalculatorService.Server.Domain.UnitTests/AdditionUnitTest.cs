using CalculatorService.Server.Domain.Calculations;
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

            //act
            Action act = () => new Addition(operands);

            //asserts
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void AdditionJustOneOperand()
        {
            //setup
            double[] operands = new double[] { 1 };

            //act
            Action act = () => new Addition(operands);

            //asserts
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void AdditionIntegers()
        {
            //setup
            double[] operands = new double[] { 1, 1 };

            //act
            Addition addition = new(operands);

            //asserts
            addition.Value.Should().Be(2);
        }

        [Fact]
        public void AdditionDoubles()
        {
            //setup
            double[] operands = new double[] { 1.5, 1.5 };

            //act
            Addition addition = new(operands);

            //asserts
            addition.Value.Should().Be(3);
        }

        [Fact]
        public void AdditionNegatives()
        {
            //setup
            double[] operands = new double[] { -1, -1 };

            //act
            Addition addition = new(operands);

            //asserts
            addition.Value.Should().Be(-2);
        }

    }
}