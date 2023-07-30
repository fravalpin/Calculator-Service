using CalculatorService.Server.Domain.Calculations;
using CalculatorService.Server.Domain.Journal;
using System;

namespace CalculatorService.Server.Application.Abstractions
{
    public interface IJournalService
    {
        void Add(ICalculation calculation, string trackingID);
        IEnumerable<OperationJournal>? Get(string trackingID);
    }
}
