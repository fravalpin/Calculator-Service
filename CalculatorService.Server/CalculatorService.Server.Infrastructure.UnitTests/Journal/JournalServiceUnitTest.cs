using CalculatorService.Server.Domain.Calculations;
using CalculatorService.Server.Domain.Journal;
using CalculatorService.Server.Infrastructure.Journal;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CalculatorService.Server.Infrastructure.UnitTests.Journal
{
    public class JournalServiceUnitTest
    {
        [Fact]
        public void AddToJournalAddition()
        {
            //setup
            Mock<ILogger<JournalService>> logger = new();
            const string user = "user";
            JournalService journalService = new(logger.Object);

            //act
            Addition addition = new(new double[] { 2, 2 });
            journalService.Add(addition, user);

            //assert
            IEnumerable<OperationJournal>? operations = journalService.Get(user);
            operations.Should().NotBeNull();
            operations?.Count().Should().Be(1);
            operations?.FirstOrDefault()?.Calculation.Should().Be(addition.ToString());
            operations?.FirstOrDefault()?.Operation.Should().Be("Sum");
        }

        [Fact]
        public void AddToJournalFactor()
        {
            //setup
            Mock<ILogger<JournalService>> logger = new();
            const string user = "user";
            JournalService journalService = new(logger.Object);

            //act
            Factor factor = new(new double[] { 2, 2 });
            journalService.Add(factor, user);

            //assert
            IEnumerable<OperationJournal>? operations = journalService.Get(user);
            operations.Should().NotBeNull();
            operations?.Count().Should().Be(1);
            operations?.FirstOrDefault()?.Calculation.Should().Be(factor.ToString());
            operations?.FirstOrDefault()?.Operation.Should().Be(factor.Operation);
        }

        [Fact]
        public void AddToJournalSubtraction()
        {
            //setup
            Mock<ILogger<JournalService>> logger = new();
            const string user = "user";
            JournalService journalService = new(logger.Object);

            //act
            Subtraction subtraction = new(4, -7);
            journalService.Add(subtraction, user);

            //assert
            IEnumerable<OperationJournal>? operations = journalService.Get(user);
            operations.Should().NotBeNull();
            operations?.Count().Should().Be(1);
            operations?.FirstOrDefault()?.Calculation.Should().Be(subtraction.ToString());
            operations?.FirstOrDefault()?.Operation.Should().Be(subtraction.Operation);
        }

        [Fact]
        public void AddToJournalDivision()
        {
            //setup
            Mock<ILogger<JournalService>> logger = new();
            const string user = "user";
            JournalService journalService = new(logger.Object);

            //act
            Division division = new(5, 2);
            journalService.Add(division, user);

            //assert
            IEnumerable<OperationJournal>? operations = journalService.Get(user);
            operations.Should().NotBeNull();
            operations?.Count().Should().Be(1);
            operations?.FirstOrDefault()?.Calculation.Should().Be(division.ToString());
            operations?.FirstOrDefault()?.Operation.Should().Be(division.Operation);
        }

        [Fact]
        public void AddToJournalSquareRoot()
        {
            //setup
            Mock<ILogger<JournalService>> logger = new();
            const string user = "user";
            JournalService journalService = new(logger.Object);

            //act
            SquareRoot squareRoot = new(16);
            journalService.Add(squareRoot, user);

            //assert
            IEnumerable<OperationJournal>? operations = journalService.Get(user);
            operations.Should().NotBeNull();
            operations?.Count().Should().Be(1);
            operations?.FirstOrDefault()?.Calculation.Should().Be(squareRoot.ToString());
            operations?.FirstOrDefault()?.Operation.Should().Be(squareRoot.Operation);
        }


        [Fact]
        public void AddToJournalTwoAddition()
        {
            //setup
            Mock<ILogger<JournalService>> logger = new();
            const string user = "user";
            JournalService journalService = new(logger.Object);

            //act
            Addition addition = new(new double[] { 2, 2 });
            journalService.Add(addition, user);
            Addition addition2 = new(new double[] { 8, 2 });
            journalService.Add(addition2, user);

            //assert
            List<OperationJournal>? operations = journalService.Get(user) as List<OperationJournal>;
            operations.Should().NotBeNull();
            operations?.Count().Should().Be(2);
            operations?[0].Calculation.Should().Be(addition.ToString());
            operations?[0].Operation.Should().Be(addition.Operation);
            operations?[1].Calculation.Should().Be(addition2.ToString());
            operations?[1].Operation.Should().Be(addition.Operation);
        }

        [Fact]
        public void AddToJournalOperationForTwoUser()
        {
            //setup
            Mock<ILogger<JournalService>> logger = new();
            const string user = "user";
            const string user2 = "user2";
            JournalService journalService = new(logger.Object);

            //act
            Addition addition = new(new double[] { 2, 2 });
            journalService.Add(addition, user);
            Addition addition2 = new(new double[] { 8, 2 });
            journalService.Add(addition2, user2);

            //assert
            List<OperationJournal>? operations = journalService.Get(user) as List<OperationJournal>;
            operations.Should().NotBeNull();
            operations?.Count().Should().Be(1);
            operations?[0].Calculation.Should().Be(addition.ToString());
            operations?[0].Operation.Should().Be(addition.Operation);

            List<OperationJournal>? operations2 = journalService.Get(user2) as List<OperationJournal>;
            operations2.Should().NotBeNull();
            operations2?.Count().Should().Be(1);
            operations2?[0].Calculation.Should().Be(addition2.ToString());
            operations2?[0].Operation.Should().Be(addition2.Operation);
        }

        [Fact]
        public void GetWithoutOperations()
        {
            //setup
            Mock<ILogger<JournalService>> logger = new();
            const string user = "user";
            JournalService journalService = new(logger.Object);

            //act
            IEnumerable<OperationJournal>? operations = journalService.Get(user);

            //assert
            operations.Should().BeNull();
        }

        [Fact]
        public void GetWithOperationButNotForID()
        {
            //setup
            Mock<ILogger<JournalService>> logger = new();
            const string user = "user";
            JournalService journalService = new(logger.Object);
            SquareRoot squareRoot = new(16);
            journalService.Add(squareRoot, user);

            //act
            IEnumerable<OperationJournal>? operations = journalService.Get("anotherUser");

            //assert
            operations.Should().BeNull();
        }

    }
}
