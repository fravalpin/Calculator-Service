using CalculatorService.Server.Domain.Calculations;
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

            //act
            Action act = () => new Factor(factors);

            //asserts
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void FactorJustOneOperand()
        {
            //setup
            double[] factors = new double[] { 1 };

            //act
            Action act = () => new Factor(factors);

            //asserts
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void FactorIntegers()
        {
            //setup
            double[] factors = new double[] { 1, 1 };

            //act
            Factor factor = new(factors);

            //asserts
            factor.Value.Should().Be(1);
        }

        [Fact]
        public void FactorDoubles()
        {
            //setup
            double[] factors = new double[] { 1.5, 1.5 };

            //act
            Factor factor = new(factors);

            //asserts
            factor.Value.Should().Be(2.25);
        }

        [Fact]
        public void FactorNegatives()
        {
            //setup
            double[] factors = new double[] { -1, -1 };

            //act
            Factor factor = new(factors);

            //asserts
            factor.Value.Should().Be(1);
        }

        [Fact]
        public void FactorExample()
        {
            //setup
            double[] factors = new double[] { 8, 3, 2 };

            //act
            Factor factor = new(factors);

            //asserts
            factor.Value.Should().Be(48);
        }

    }
}