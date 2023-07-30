using CalculatorService.Server.Domain.Calculations;
using CalculatorService.Server.Domain.Journal;
using FluentAssertions;
using Xunit;

namespace CalculatorService.Server.Domain.UnitTests.Journal
{
    public class OperationJournalUnitTest
    {
        [Fact]
        public void JounalCreateAddition()
        {
            Addition addition = new(new double[] { 2, 2 });
            OperationJournal operationJournal = new(addition);

            operationJournal.Calculation.Should().Be("2 + 2 = 4");
            operationJournal.Operation.Should().Be("Sum");
            operationJournal.Date.Day.Should().Be(DateTime.Now.Day);
        }

        [Fact]
        public void JounalCreateSubtraction()
        {
            Subtraction subtraction = new(3 ,-7);
            OperationJournal operationJournal = new(subtraction);

            operationJournal.Calculation.Should().Be("3 -7 = -4");
            operationJournal.Operation.Should().Be("Sub");
            operationJournal.Date.Day.Should().Be(DateTime.Now.Day);
        }

        [Fact]
        public void JounalCreateFactor()
        {
            Factor factor = new(new double[] { 2, 2 });
            OperationJournal operationJournal = new(factor);

            operationJournal.Calculation.Should().Be("2 x 2 = 4");
            operationJournal.Operation.Should().Be("Mul");
            operationJournal.Date.Day.Should().Be(DateTime.Now.Day);
        }

        [Fact]
        public void JounalCreateDivision()
        {
            Division division = new(5 ,2);
            OperationJournal operationJournal = new(division);

            operationJournal.Calculation.Should().Be("5 / 2 = q:2 r:1");
            operationJournal.Operation.Should().Be("Div");
            operationJournal.Date.Day.Should().Be(DateTime.Now.Day);
        }

    }
}
