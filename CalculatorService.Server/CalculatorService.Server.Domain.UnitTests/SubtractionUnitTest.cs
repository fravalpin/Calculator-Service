using CalculatorService.Server.Domain.Calculations;
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

            //act
            Subtraction subtraction = new(minuend, subtrahend);

            //asserts
            subtraction.Value.Should().Be(0);
        }

        [Fact]
        public void SubtractionDoubles()
        {
            //setup
            double minuend = 1.5;
            double subtrahend = -1.5;

            //act
            Subtraction subtraction = new(minuend, subtrahend);

            //asserts
            subtraction.Value.Should().Be(0);
        }

        [Fact]
        public void SubtractionNegatives()
        {
            //setup
            double minuend = -1;
            double subtrahend = -1;

            //act
            Subtraction subtraction = new(minuend, subtrahend);

            //asserts
            subtraction.Value.Should().Be(-2);
        }

        [Fact]
        public void SubtractionPositiveNegative()
        {
            //setup
            double minuend = 3;
            double subtrahend = -7;

            //act
            Subtraction subtraction = new(minuend, subtrahend);

            //asserts
            subtraction.Value.Should().Be(-4);
        }

        [Fact]
        public void SubtractionNegativePositive()
        {
            //setup
            double minuend = -1;
            double subtrahend = 1;

            //act
            Subtraction subtraction = new(minuend, subtrahend);

            //asserts
            subtraction.Value.Should().Be(0);
        }


    }
}