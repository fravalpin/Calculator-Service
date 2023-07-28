using FluentAssertions;
using Xunit;

namespace CalculatorService.Server.Domain.UnitTests
{
    public class FactorUnitTest
    {

        [Fact]
        public void FactorNotfactors()
        {
            //setup
            double[] factors = null;
            Factor factor = new(factors);

            //act
            Action act = () => factor.Calculate();

            //asserts
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void FactorJustOneOperand()
        {
            //setup
            double[] factors = new double[] { 1 };
            Factor factor = new(factors);

            //act
            Action act = () => factor.Calculate();

            //asserts
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void FactorIntegers()
        {
            //setup
            double[] factors = new double[] { 1, 1 };
            Factor factor = new(factors);

            //act
            double result = factor.Calculate();

            //asserts
            result.Should().Be(1);
        }

        [Fact]
        public void FactorDoubles()
        {
            //setup
            double[] factors = new double[] { 1.5, 1.5 };
            Factor factor = new(factors);

            //act
            double result = factor.Calculate();

            //asserts
            result.Should().Be(2.25);
        }

        [Fact]
        public void FactorNegatives()
        {
            //setup
            double[] factors = new double[] { -1, -1 };
            Factor factor = new(factors);

            //act
            double result = factor.Calculate();

            //asserts
            result.Should().Be(1);
        }

        [Fact]
        public void FactorExample()
        {
            //setup
            double[] factors = new double[] { 8, 3, 2 };
            Factor factor = new(factors);

            //act
            double result = factor.Calculate();

            //asserts
            result.Should().Be(48);
        }

    }
}