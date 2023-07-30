namespace CalculatorService.Server.Domain.Calculations
{
    public class Subtraction : ICalculation
    {
        private readonly double _minuend;
        private readonly double _subtrahend;

        public Subtraction(double minuend, double subtrahend)
        {
            //Doubt. According to the example the minuend is 3 and the subtrahend is -7 and the difference is -4. So I understand that you have to add to get this result.
            //I don't know if it was an error in the PDF or I didn't understand it correctly.
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