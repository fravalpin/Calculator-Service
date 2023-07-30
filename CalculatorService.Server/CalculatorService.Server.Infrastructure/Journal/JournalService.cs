using CalculatorService.Server.Domain.Calculations;
using CalculatorService.Server.Application.Abstractions;
using Microsoft.Extensions.Logging;
using CalculatorService.Server.Domain.Journal;

namespace CalculatorService.Server.Infrastructure.Journal
{
    public class JournalService : IJournalService
    {
        private readonly ILogger<JournalService> _logger;
        private readonly Dictionary<string, List<OperationJournal>> _journal = new Dictionary<string, List<OperationJournal>>();

        public JournalService(ILogger<JournalService> logger)
        {
            _logger = logger;
        }

        public void Add(ICalculation calculation, string trackingID)
        {
            OperationJournal operationJournal = new(calculation);
            AddOperation(trackingID, operationJournal);
        }

        private void AddOperation(string trackingID, OperationJournal operationJournal)
        {
            _journal.TryGetValue(trackingID, out List<OperationJournal>? operations);
            if (operations == null)
            {
                operations = new()
                {
                    operationJournal
                };
                _journal.Add(trackingID, operations);
            }
            else
            {
                operations.Add(operationJournal);
            }

            _logger.LogDebug($"Added new operation in journal: {operationJournal.Operation}: {operationJournal.Calculation} for trackingID {trackingID}");
        }

        public IEnumerable<OperationJournal>? Get(string trackingID)
        {
            _journal.TryGetValue(trackingID, out List<OperationJournal>? operations);
            return operations;
        }
    }
}
