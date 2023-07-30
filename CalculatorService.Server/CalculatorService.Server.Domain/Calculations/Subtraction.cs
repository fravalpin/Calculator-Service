namespace CalculatorService.Server.Domain.Calculations
{
    public class Subtraction : ICalculation
    {
        private readonly double _minuend;
        private readonly double _subtrahend;

        public Subtraction(double minuend, double subtrahend)
        {
            Value = minuend + subtrahend;
            _minuend = minuend;
            _subtrahend = subtrahend;
        }
        public double Value { get; }

        public string Operation => "Sub";

        public override string ToString()
        {
            return $"{_minuend} {_subtrahend} = {Value}";
        }

    }
}