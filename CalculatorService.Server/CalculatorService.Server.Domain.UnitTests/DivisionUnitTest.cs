using CalculatorService.Server.Domain.Calculations;
using FluentAssertions;
using Xunit;

namespace CalculatorService.Server.Domain.UnitTests
{
    public class DivisionUnitTest
    {

        [Fact]
        public void DivisionIntegers()
        {
            //setup
            double dividend = 10;
            double divisor = 2;

            //act
            Division division = new(dividend, divisor);

            //asserts
            division.Quotient.Should().Be(5);
            division.Remainder.Should().Be(0);
        }

        [Fact]
        public void DivisionDoubles()
        {
            //setup
            double dividend = 10.5;
            double divisor = 1.5;

            //act
            Division division = new(dividend, divisor);

            //asserts
            division.Quotient.Should().Be(7);
            division.Remainder.Should().Be(0);
        }

        [Fact]
        public void DivisionNegatives()
        {
            //setup
            double dividend = -2;
            double divisor = -2;

            //act
            Division division = new(dividend, divisor);

            //asserts
            division.Quotient.Should().Be(1);
            division.Remainder.Should().Be(0);
        }

        [Fact]
        public void DivisionPositiveNegative()
        {
            //setup
            double dividend = 7;
            double divisor = -7;

            //act
            Division division = new(dividend, divisor);

            //asserts
            division.Quotient.Should().Be(-1);
            division.Remainder.Should().Be(0);
        }

        [Fact]
        public void DivisionNegativePositive()
        {
            //setup
            double dividend = -1;
            double divisor = 1;

            //act
            Division division = new(dividend, divisor);

            //asserts
            division.Quotient.Should().Be(-1);
            division.Remainder.Should().Be(0);
        }

        [Fact]
        public void DivisionDivisorZero()
        {
            //setup
            double dividend = 10;
            double divisor = 0;

            //act
            Action act = () => new Division(dividend, divisor);

            //asserts
            act.Should().Throw<DivideByZeroException>();
        }

        [Fact]
        public void DivisionExample()
        {
            //setup
            double dividend = 11;
            double divisor = 2;

            //act
            Division division = new(dividend, divisor);

            //asserts
            division.Quotient.Should().Be(5);
            division.Remainder.Should().Be(1);
        }

        [Fact]
        public void DivisionExampleNegative()
        {
            //setup
            double dividend = -11;
            double divisor = 2;

            //act
            Division division = new(dividend, divisor);

            //asserts
            division.Quotient.Should().Be(-5);
            division.Remainder.Should().Be(-1);
        }


        [Fact]
        public void DivisionRemainder2()
        {
            //setup
            double dividend = 17;
            double divisor = 5;

            //act
            Division division = new(dividend, divisor);

            //asserts
            division.Quotient.Should().Be(3);
            division.Remainder.Should().Be(2);
        }

    }
}