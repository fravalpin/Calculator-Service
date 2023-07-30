using CalculatorService.Server.Domain.Calculations;

namespace CalculatorService.Server.Domain.Journal
{
    public class OperationJournal
    {

        public OperationJournal(ICalculation calculation) : this(calculation.Operation, calculation.ToString())
        { }
        private OperationJournal(string operation, string calculation)
        {
            Operation = operation;
            Calculation = calculation;
            Date = DateTime.Now;
        }

        public string Operation { get; } 
        public string Calculation { get; }
        public DateTime Date { get; }
    }
}
