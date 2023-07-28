using FluentAssertions;
using Xunit;

namespace CalculatorService.Server.Domain.UnitTests
{
    public class SubtractionUnitTest
    {

        [Fact]
        public void SubtractionIntegers()
        {
            //setup
            double minuend = 1;
            double subtrahend = -1;
            Subtraction subtraction = new(minuend, subtrahend);

            //act
            double result = subtraction.Calculate();

            //asserts
            result.Should().Be(0);
        }

        [Fact]
        public void SubtractionDoubles()
        {
            //setup
            double minuend = 1.5;
            double subtrahend = -1.5;
            Subtraction subtraction = new(minuend, subtrahend);

            //act
            double result = subtraction.Calculate();

            //asserts
            result.Should().Be(0);
        }

        [Fact]
        public void SubtractionNegatives()
        {
            //setup
            double minuend = -1;
            double subtrahend = -1;
            Subtraction subtraction = new(minuend, subtrahend);

            //act
            double result = subtraction.Calculate();

            //asserts
            result.Should().Be(-2);
        }

        [Fact]
        public void SubtractionPositiveNegative()
        {
            //setup
            double minuend = 3;
            double subtrahend = -7;
            Subtraction subtraction = new(minuend, subtrahend);

            //act
            double result = subtraction.Calculate();

            //asserts
            result.Should().Be(-4);
        }

        [Fact]
        public void SubtractionNegativePositive()
        {
            //setup
            double minuend = -1;
            double subtrahend = 1;
            Subtraction subtraction = new(minuend, subtrahend);

            //act
            double result = subtraction.Calculate();

            //asserts
            result.Should().Be(0);
        }


    }
}