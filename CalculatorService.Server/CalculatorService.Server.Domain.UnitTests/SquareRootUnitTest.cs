using FluentAssertions;
using Xunit;

namespace CalculatorService.Server.Domain.UnitTests
{
    public class SquareRootUnitTest
    {
        [Fact]
        public void SquareExample()
        {
            SquareRoot square = new(16);
            square.Value.Should().Be(4);
        }

        [Fact]
        public void SquareExampleNegative()
        {
            Action act = () => new SquareRoot (-16);
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void SquareDouble()
        {
            SquareRoot square = new(3);
            square.Value.Should().Be(1.7320508075688772);
        }

    }
}